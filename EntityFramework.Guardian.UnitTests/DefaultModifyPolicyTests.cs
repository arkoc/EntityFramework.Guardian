using EntityFramework.Guardian.Models;
using EntityFramework.Guardian.Policies;
using EntityFramework.Guardian.UnitTests.Models;
using Xunit;

namespace EntityFramework.Guardian.UnitTests
{

    public class DefaultModifyPolicyTests
    {
        private GuardianKernel _kernel;
        private DefaultModifyPolicy _policy;

        public DefaultModifyPolicyTests()
        {
            _kernel = new GuardianKernel();
            _policy = new DefaultModifyPolicy();
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

            var policyContext = GetPolicyContext(model, AccessTypes.Update);

            var result = _policy.Apply(policyContext, _kernel);

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
                AccessType = AccessTypes.Update,
                EntityTypeName = model.GetType().Name
            });
            _kernel.Permissions = permissions;

            var policyContext = GetPolicyContext(model, AccessTypes.Update);

            var result = _policy.Apply(policyContext, _kernel);

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
                AccessType = AccessTypes.Add,
                EntityTypeName = model.GetType().Name,
                RowIdentifier = "1"
            });

            _kernel.Permissions = permissions;

            var policyContext = GetPolicyContext(model, AccessTypes.Add);

            var result = _policy.Apply(policyContext, _kernel);

            Assert.Equal(true, result);
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

            var permissions = new GuardianPermissions();

            permissions.RowLevel.Add(new TestRowPermission()
            {
                AccessType = AccessTypes.Add,
                EntityTypeName = model.GetType().Name,
                RowIdentifier = "1"
            });

            permissions.ColumnLevel.Add(new TestColumnRestriction()
            {
                AccessType = AccessTypes.Add,
                EntityTypeName = model.GetType().Name,
                PropertyName = "Property1"
            });

            _kernel.Permissions = permissions;

            var policyContext = GetPolicyContext(model, AccessTypes.Add);

            var result = _policy.Apply(policyContext, _kernel);

            Assert.Equal(false, result);

        }

        private ModifyPolicyContext GetPolicyContext(IProtectableObject model, AccessTypes accessType)
        {
            return new ModifyPolicyContext()
            {
                AccessType = accessType,
                Entity = model,
                EntityRowKey = _kernel.EntityKeyProvider.GetKey(model),
                EntityTypeName = model.GetType().Name,
                ModifiedProperties = { "Property1" }
            };
        }
    }
}
