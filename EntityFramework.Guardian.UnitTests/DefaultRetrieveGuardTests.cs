﻿// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Guards;
using EntityFramework.Guardian.Tests.Models;
using EntityFramework.Guardian.UnitTests.Dummy;
using EntityFramework.Guardian.Entities;
using Xunit;
using EntityFramework.Guardian.Extensions;

namespace EntityFramework.Guardian.UnitTests
{
    public class DefaultRetrieveGuardTests
    {
        private DefaultRetrieveGuard _guard;

        public DefaultRetrieveGuardTests()
        {
            var kernel = new GuardianKernel();
            kernel.UseInMemoryPermission(new TestPermission()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = typeof(Model1).Name
            });

            _guard = new DefaultRetrieveGuard(kernel);
        }

        [Fact]
        public void Protect_ShouldAllow()
        {
            var model = new Model1();
            _guard.Protect(new RetrieveGuardContext()
            {
                Entry = new DummyObjectAccessEntry(model, AccessTypes.Get)
            });

            Assert.Equal(ProtectionResults.Allow, model.ProtectionResult);
        }

        [Fact]
        public void Protect_ShouldDeny()
        {
            var model = new Model2() { Id = "1" };
            _guard.Protect(new RetrieveGuardContext()
            {
                Entry = new DummyObjectAccessEntry(model, AccessTypes.Get)
            });

            Assert.Equal(ProtectionResults.Deny, model.ProtectionResult);
        }


        [Fact]
        public void Protect_ShouldCallUserDefinedPolicy()
        {
            var kernel = new GuardianKernel();
            kernel.UseInMemoryPermission(new TestPermission()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = typeof(Model1).Name
            });

            kernel.Policies.RetrievePolicies.Add(new DummyRetrievePolicy());

            var guard = new DefaultRetrieveGuard(kernel);

            var model = new Model1() { Id = 1 };
            guard.Protect(new RetrieveGuardContext()
            {
                Entry = new DummyObjectAccessEntry(model, AccessTypes.Get)
            });

            Assert.Equal(ProtectionResults.Deny, model.ProtectionResult);
        }
    }
}
