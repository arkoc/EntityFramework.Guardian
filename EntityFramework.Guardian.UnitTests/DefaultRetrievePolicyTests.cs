using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Extensions;
using EntityFramework.Guardian.Policies;
using EntityFramework.Guardian.UnitTests.Models;
using Xunit;

namespace EntityFramework.Guardian.UnitTests
{

    public class DefaultRetrievePolicyTests
    {
        private GuardianKernel _kernel;
        private DefaultRetrievePolicy _policy;

        public DefaultRetrievePolicyTests()
        {
            _kernel = new GuardianKernel();
            _policy = new DefaultRetrievePolicy();
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

            var policyContext = GetPolicyContext(model);

            var result = _policy.Check(policyContext, _kernel);

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

            _kernel.UsePermission(new TestPermission()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = model.GetType().Name
            });

            var policyContext = GetPolicyContext(model);

            var result = _policy.Check(policyContext, _kernel);

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

            _kernel.UsePermission(new TestRowPermission()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = model.GetType().Name,
                RowIdentifier = "1"
            });

            var policyContext = GetPolicyContext(model);

            var result = _policy.Check(policyContext, _kernel);

            Assert.Equal(true, result.IsSuccess);
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

            _kernel.UsePermission(permission);

            var policyContext = GetPolicyContext(model);

            var result = _policy.Check(policyContext, _kernel);

            Assert.Equal(true, result.IsSuccess);

            Assert.Equal(true, result.RestrictedProperties.Contains("Property1"));

        }

        private RetrievePolicyContext GetPolicyContext(IProtectableObject model)
        {
            return new RetrievePolicyContext()
            {
                Entity = model,
                EntityRowKey = _kernel.EntityKeyProvider.GetKey(model),
                EntityTypeName = model.GetType().Name
            };
        }
    }
}
