using EntityFramework.Guardian.Guards;
using EntityFramework.Guardian.UnitTests.Models;
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
            kernel.UsePermission(new TestPermission()
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
            _guard.Protect(new RetrieveProtectionContext()
            {
                Entry = new DummyObjectAccessEntry(AccessTypes.Get, model)
            });

            Assert.Equal(ProtectionResults.Allow, model.ProtectionResult);
        }

        [Fact]
        public void Protect_ShouldDeny()
        {
            var model = new Model2() { Id = "1" };
            _guard.Protect(new RetrieveProtectionContext()
            {
                Entry = new DummyObjectAccessEntry(AccessTypes.Get, model)
            });

            Assert.Equal(ProtectionResults.Deny, model.ProtectionResult);
        }


        [Fact]
        public void Protect_ShouldCallUserDefinedPolicy()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermission(new TestPermission()
            {
                AccessType = AccessTypes.Get,
                EntityTypeName = typeof(Model1).Name
            });

            kernel.RetrieveProtectionPolicies.Add(new DummyRetrievePolicy());

            var guard = new DefaultRetrieveGuard(kernel);

            var model = new Model1() { Id = 1 };
            guard.Protect(new RetrieveProtectionContext()
            {
                Entry = new DummyObjectAccessEntry(AccessTypes.Get, model)
            });

            Assert.Equal(ProtectionResults.Deny, model.ProtectionResult);
        }
    }
}
