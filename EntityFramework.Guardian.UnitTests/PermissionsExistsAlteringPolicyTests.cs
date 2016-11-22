// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Extensions;
using EntityFramework.Guardian.Policies;
using EntityFramework.Guardian.UnitTests.Dummy;
using EntityFramework.Guardian.Tests.Models;
using System.Collections.Generic;
using Xunit;

namespace EntityFramework.Guardian.UnitTests
{
    public class PermissionsExistsAlteringPolicyTests
    {
        private PermissionsExistsAlteringPolicy _policy;

        public PermissionsExistsAlteringPolicyTests()
        {
            _policy = new PermissionsExistsAlteringPolicy();
        }

        [Fact]
        public void Check_WithNoPermissions_ShouldReturnFalse()
        {
            var model = new Model1()
            {
                Id = 1,
                Property1 = "Property1",
                Property2 = "Property2"
            };

            var kernel = new GuardianKernel();

            var policyContext = GetPolicyContext(kernel, model, AccessTypes.Update);

            var result = _policy.Check(policyContext);

            Assert.Equal(false, result.IsSuccess);
        }

        [Fact]
        public void Check_WithGeneralPermission_ShouldReturnTrue()
        {
            var model = new Model1()
            {
                Id = 1,
                Property1 = "Property1",
                Property2 = "Property2"
            };

            var kernel = new GuardianKernel();

            kernel.UseInMemoryPermission(new TestPermission()
            {
                AccessType = AccessTypes.Update,
                EntityTypeName = model.GetType().Name
            });

            var policyContext = GetPolicyContext(kernel, model, AccessTypes.Update);

            var result = _policy.Check(policyContext);

            Assert.Equal(true, result.IsSuccess);
        }

        [Fact]
        public void Check_WithRowLevelPermission_ShouldReturnTrue()
        {
            var model = new Model1()
            {
                Id = 1,
                Property1 = "Property1",
                Property2 = "Property2"
            };

            var kernel = new GuardianKernel();

            kernel.UseInMemoryPermission(new TestRowPermission()
            {
                AccessType = AccessTypes.Add,
                EntityTypeName = model.GetType().Name,
                RowIdentifier = "1"
            });


            var policyContext = GetPolicyContext(kernel, model, AccessTypes.Add);

            var result = _policy.Check(policyContext);

            Assert.Equal(true, result.IsSuccess);
        }

        private AlteringPolicyContext GetPolicyContext(GuardianKernel kernel, IProtectableObject model, AccessTypes accessType, List<string> modifiedProperties = null)
        {
            modifiedProperties = modifiedProperties ?? new List<string>();
            var entry = new DummyObjectAccessEntry(model, accessType);

            return AlteringPolicyContext.For(kernel, entry, modifiedProperties);
        }
    }
}
