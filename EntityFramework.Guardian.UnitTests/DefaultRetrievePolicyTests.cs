using EntityFramework.Guardian;
using EntityFramework.Guardian.Models;
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
            var permissions = new GuardianPermissions();
            _kernel.Permissions = permissions;

            var model = new Model1()
            {
                Id = 1,
                Property1 = "Property1",
                Property2 = "Property2"
            };

            var policyContext = GetPolicyContext(model);

            var result = _policy.Check(policyContext, _kernel);

            Assert.Equal(false, result);
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

            var permissions = new GuardianPermissions();
            permissions.General.Add(new TestPermission()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = model.GetType().Name
            });
            _kernel.Permissions = permissions;

            var policyContext = GetPolicyContext(model);

            var result = _policy.Check(policyContext, _kernel);

            Assert.Equal(true, result);

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

            var permissions = new GuardianPermissions();
            permissions.RowLevel.Add(new TestRowPermission()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = model.GetType().Name,
                RowIdentifier = "1"
            });

            _kernel.Permissions = permissions;

            var policyContext = GetPolicyContext(model);

            var result = _policy.Check(policyContext, _kernel);

            Assert.Equal(true, result);
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

            var permissions = new GuardianPermissions();

            permissions.RowLevel.Add(new TestRowPermission()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = model.GetType().Name,
                RowIdentifier = "1"
            });

            permissions.ColumnLevel.Add(new TestColumnRestriction()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = model.GetType().Name,
                PropertyName = "Property1"
            });

            _kernel.Permissions = permissions;

            var policyContext = GetPolicyContext(model);

            var result = _policy.Check(policyContext, _kernel);

            Assert.Equal(true, result);

            Assert.Equal(true, model.ProtectedProperties.Contains("Property1"));

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
