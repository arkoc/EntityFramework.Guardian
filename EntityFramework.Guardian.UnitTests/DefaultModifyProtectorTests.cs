using EntityFramework.Guardian.Protection;
using EntityFramework.Guardian.UnitTests.Models;
using EntityFramework.Guardian.UnitTests.Dummy;
using EntityFramework.Guardian.Models;
using Xunit;
using EntityFramework.Guardian.Exceptions;

namespace EntityFramework.Guardian.UnitTests
{
    public class DefaultModifyProtectorTests
    {
        private DefaultModifyProtector _protector;

        public DefaultModifyProtectorTests()
        {
            var kernel = new GuardianKernel();
            kernel.Permissions.General.Add(new TestPermission()
            {
                AccessType = AccessTypes.Add,
                EntityTypeName = typeof(Model1).Name
            });

            kernel.Permissions.ColumnLevel.Add(new TestColumnRestriction()
            {
                AccessType = AccessTypes.Update,
                EntityTypeName = typeof(Model1).Name,
                PropertyName = "Property1"
            });

            _protector = new DefaultModifyProtector(kernel);
        }

        [Fact]
        public void Protect_ShouldAllow()
        {
            var model = new Model1();
            _protector.Protect(new ModifyProtectionContext()
            {
                Entry = new DummyObjectAccessEntry(AccessTypes.Add, model),
                ModifiedProperties = {"Property1"}
            });
        }

        [Fact]
        public void Protect_ShouldDeny1()
        {
            var model = new Model2() { Id = "1" };
            Assert.Throws(typeof(AccessDeniedException), () =>
            {
                _protector.Protect(new ModifyProtectionContext()
                {
                    Entry = new DummyObjectAccessEntry(AccessTypes.Delete, model),
                    ModifiedProperties = { "Property1" }
                });
            });
        }


        [Fact]
        public void Protect_ShouldDeny2()
        {
            var model = new Model2() { Id = "1" };
            Assert.Throws(typeof(AccessDeniedException), () =>
            {
                _protector.Protect(new ModifyProtectionContext()
                {
                    Entry = new DummyObjectAccessEntry(AccessTypes.Update, model),
                    ModifiedProperties = { "Property1" }
                });
            });
        }


        [Fact]
        public void Protect_ShouldCallUserDefinedPolicy()
        {
            var kernel = new GuardianKernel();
            kernel.Permissions.General.Add(new TestPermission()
            {
                AccessType = AccessTypes.Add,
                EntityTypeName = typeof(Model1).Name
            });
            kernel.ModifyProtectionPolicies.Add(new DummyModifyPolicy());

            var protector = new DefaultModifyProtector(kernel);

            var model = new Model1() { Id = 1 };

            Assert.Throws(typeof(AccessDeniedException), () =>
            {
                protector.Protect(new ModifyProtectionContext()
                {
                    Entry = new DummyObjectAccessEntry(AccessTypes.Add, model),
                    ModifiedProperties = { "Property1" }
                });
            });
        }
    }
}
