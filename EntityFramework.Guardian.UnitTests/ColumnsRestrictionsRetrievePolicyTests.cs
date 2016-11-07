﻿using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Extensions;
using EntityFramework.Guardian.Policies;
using EntityFramework.Guardian.UnitTests.Dummy;
using EntityFramework.Guardian.UnitTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EntityFramework.Guardian.UnitTests
{
    public class ColumnsRestrictionsRetrievePolicyTests
    {
        private ColumnsRestrictionsRetrievePolicy _policy;

        public ColumnsRestrictionsRetrievePolicyTests()
        {
            _policy = new ColumnsRestrictionsRetrievePolicy();
        }

        [Fact]
        public void Check_WithColumnRestrictions_ShouldReturnRestricted()
        {
            var model = new Model1()
            {
                Id = 1,
                Property1 = "Property1",
                Property2 = "Property2"
            };

            var permission = new TestRowPermission()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = model.GetType().Name,
                RowIdentifier = "1"
            };

            permission.ColumnRestrictions.Add(new TestColumnRestriction()
            {
                PropertyName = "Property1"
            });

            var kernel = new GuardianKernel();

            kernel.UsePermission(permission);

            var policyContext = GetPolicyContext(kernel, model);

            var result = _policy.Check(policyContext);

            Assert.Equal(true, result.IsSuccess);

            Assert.Equal(true, result.RestrictedProperties.Contains("Property1"));

        }


        private RetrievePolicyContext GetPolicyContext(GuardianKernel kernel, IProtectableObject model)
        {
            var entry = new DummyObjectAccessEntry(model, AccessTypes.Get);
            return RetrievePolicyContext.For(kernel, entry);
        }
    }
}