using EntityFramework.Guardian.Core;
using EntityFramework.Guardian.Core.Models;
using EntityFramework.Guardian.Core.Policies;
using EntityFramework.Guardian.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntityFramework.Guardian.Tests
{
    [TestClass]
    public class DefaultRetrievePolicyTests
    {
        private GuardianKernel _kernel;
        private DefaultRetrievePolicy _policy;

        public DefaultRetrievePolicyTests()
        {
            _kernel = new GuardianKernel();
            _policy = new DefaultRetrievePolicy();
        }

        [TestMethod]
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

            Assert.AreEqual(false, result);
        }

        [TestMethod]
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

            Assert.AreEqual(true, result);

        }

        [TestMethod]
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

            Assert.AreEqual(true, result);
        }

        [TestMethod]
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

            Assert.AreEqual(true, result);

            Assert.AreEqual(true, model.ProtectedProperties.Contains("Property1"));

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
