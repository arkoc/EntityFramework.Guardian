using EntityFramework.Guardian.Protection;
using EntityFramework.Guardian.UnitTests.Models;
using EntityFramework.Guardian.UnitTests.Dummy;
using EntityFramework.Guardian.Models;
using Xunit;

namespace EntityFramework.Guardian.UnitTests
{
    public class DefaultRetrieveProtectorTests
    {
        private DefaultRetrieveProtector _protector;

        public DefaultRetrieveProtectorTests()
        {
            var kernel = new GuardianKernel();
            kernel.Permissions.General.Add(new TestPermission()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = typeof(Model1).Name
            });

            _protector = new DefaultRetrieveProtector(kernel);
        }

        [Fact]
        public void Protect_ShouldAllow()
        {
            var model = new Model1();
            _protector.Protect(new RetrieveProtectionContext()
            {
                Entry = new DummyObjectAccessEntry(AccessTypes.Get, model)
            });

            Assert.Equal(ProtectionResults.Allow, model.ProtectionResult);
        }

        [Fact]
        public void Protect_ShouldDeny()
        {
            var model = new Model2() { Id = "1" };
            _protector.Protect(new RetrieveProtectionContext()
            {
                Entry = new DummyObjectAccessEntry(AccessTypes.Get, model)
            });

            Assert.Equal(ProtectionResults.Deny, model.ProtectionResult);
        }


        [Fact]
        public void Protect_ShouldCallUserDefinedPolicy()
        {
            var kernel = new GuardianKernel();
            kernel.Permissions.General.Add(new TestPermission()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = typeof(Model1).Name
            });
            kernel.RetrieveProtectionPolicies.Add(new DummyRetrievePolicy());

            var protector = new DefaultRetrieveProtector(kernel);

            var model = new Model1() { Id = 1 };
            _protector.Protect(new RetrieveProtectionContext()
            {
                Entry = new DummyObjectAccessEntry(AccessTypes.Get, model)
            });

            Assert.Equal(ProtectionResults.Deny, model.ProtectionResult);
        }
    }
}
