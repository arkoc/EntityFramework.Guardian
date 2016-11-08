using EntityFramework.Guardian.Guards;
using EntityFramework.Guardian.UnitTests.Models;
using EntityFramework.Guardian.UnitTests.Dummy;
using EntityFramework.Guardian.Entities;
using Xunit;
using EntityFramework.Guardian.Exceptions;
using EntityFramework.Guardian.Extensions;

namespace EntityFramework.Guardian.UnitTests
{
    public class DefaultModifyGuardTests
    {
        private DefaultModifyGuard _guard;

        public DefaultModifyGuardTests()
        {
            var kernel = new GuardianKernel();

            var permission = new TestPermission()
            {
                AccessType = AccessTypes.Add,
                EntityTypeName = typeof(Model1).Name
            };

            permission.ColumnRestrictions.Add(new TestColumnRestriction()
            {
                PropertyName = "Property1"
            });

            kernel.UsePermission(permission);

            _guard = new DefaultModifyGuard(kernel);
        }

        [Fact]
        public void Protect_ShouldAllow()
        {
            var model = new Model1();
            _guard.Protect(new ModifyGuardContext()
            {
                Entry = new DummyObjectAccessEntry(model, AccessTypes.Add),
                ModifiedProperties = {"Property2"}
            });
        }

        [Fact]
        public void Protect_ShouldDeny1()
        {
            var model = new Model2() { Id = "1" };
            Assert.Throws(typeof(AccessDeniedException), () =>
            {
                _guard.Protect(new ModifyGuardContext()
                {
                    Entry = new DummyObjectAccessEntry(model, AccessTypes.Delete),
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
                _guard.Protect(new ModifyGuardContext()
                {
                    Entry = new DummyObjectAccessEntry(model, AccessTypes.Update),
                    ModifiedProperties = { "Property1" }
                });
            });
        }


        [Fact]
        public void Protect_ShouldCallUserDefinedPolicy()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermission(new TestPermission()
            {
                AccessType = AccessTypes.Add,
                EntityTypeName = typeof(Model1).Name
            });
            kernel.Policies.ModifyPolicies.Add(new DummyModifyPolicy());

            var guard = new DefaultModifyGuard(kernel);

            var model = new Model1() { Id = 1 };

            Assert.Throws(typeof(AccessDeniedException), () =>
            {
                guard.Protect(new ModifyGuardContext()
                {
                    Entry = new DummyObjectAccessEntry(model, AccessTypes.Add),
                    ModifiedProperties = { "Property1" }
                });
            });
        }
    }
}
