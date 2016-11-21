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
    public class ColumnsRestrictionsModifyPolicyTests
    {
        private ColumnsRestrictionsModifyPolicy _policy;

        public ColumnsRestrictionsModifyPolicyTests()
        {
            _policy = new ColumnsRestrictionsModifyPolicy();
        }

        [Fact]
        public void Check_WithColumnRestrictions_ShouldReturnFalse()
        {
            var model = new Model1()
            {
                Id = 1,
                Property1 = "Property1",
                Property2 = "Property2"
            };

            var rowPermission = new TestRowPermission()
            {
                AccessType = AccessTypes.Add,
                EntityTypeName = model.GetType().Name,
                RowIdentifier = "1"
            };

            rowPermission.ColumnRestrictions.Add(new TestColumnRestriction()
            {
                PropertyName = "Property1"
            });

            var kernel = new GuardianKernel();

            kernel.UseInMemoryPermission(rowPermission);

            var policyContext = GetPolicyContext(kernel, model, AccessTypes.Add, new List<string> { "Property1" });

            var result = _policy.Check(policyContext);

            Assert.Equal(false, result.IsSuccess);
        }


        private ModifyPolicyContext GetPolicyContext(GuardianKernel kernel, IProtectableObject model, AccessTypes accessType, List<string> modifiedProperties = null)
        {
            modifiedProperties = modifiedProperties ?? new List<string>();
            var entry = new DummyObjectAccessEntry(model, accessType);

            return ModifyPolicyContext.For(kernel, entry, modifiedProperties);
        }
    }
}
