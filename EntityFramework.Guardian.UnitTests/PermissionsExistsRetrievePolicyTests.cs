using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Extensions;
using EntityFramework.Guardian.Policies;
using EntityFramework.Guardian.UnitTests.Dummy;
using EntityFramework.Guardian.UnitTests.Models;
using Xunit;

namespace EntityFramework.Guardian.UnitTests
{

    public class PermissionsExistsRetrievePolicyTests
    {
        private PermissionExistsRetrievePolicy _policy;

        public PermissionsExistsRetrievePolicyTests()
        {
            _policy = new PermissionExistsRetrievePolicy();
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
            var policyContext = GetPolicyContext(kernel, model);

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

            kernel.UsePermission(new TestPermission()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = model.GetType().Name
            });

            var policyContext = GetPolicyContext(kernel, model);

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

            kernel.UsePermission(new TestRowPermission()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = model.GetType().Name,
                RowIdentifier = "1"
            });

            var policyContext = GetPolicyContext(kernel, model);

            var result = _policy.Check(policyContext);

            Assert.Equal(true, result.IsSuccess);
        }

        private RetrievePolicyContext GetPolicyContext(GuardianKernel kernel, IProtectableObject model)
        {
            var entry = new DummyObjectAccessEntry(model, AccessTypes.Get);
            return RetrievePolicyContext.For(kernel, entry);
        }
    }
}
