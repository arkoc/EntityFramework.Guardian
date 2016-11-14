using System.Collections.Generic;
using System.Linq;
using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Extensions;
using Xunit;
using EntityFramework.Guardian.Exceptions;
using EntityFramework.Guardian.Tests.Models;
using EntityFramework.Guardian.IntegrationTests.Database;

namespace EntityFramework.Guardian.IntegrationTests
{
    public class UpdateIntegrationTests : IntegrationTestsBase
    {
        [Fact]
        public void EntityUpdate_Role_NoAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                UpdateModel1(dataContext);
                Assert.Throws<AccessDeniedException>(() => 
                {
                    dataContext.SaveChanges();
                });
            }
        }

        [Fact]
        public void EntityUpdate_Role_HaveAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestPermission()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model1).Name
                });

                UpdateModel1(dataContext);
                dataContext.SaveChanges();;
            }
        }

        [Fact]
        public void EntityUpdate_Role_Property_NoAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                var permission = new TestPermission()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model1).Name
                };

                permission.ColumnRestrictions.Add(new TestColumnRestriction()
                {
                    PropertyName = "Property1"
                });

                permission.ColumnRestrictions.Add(new TestColumnRestriction()
                {
                    PropertyName = "Property2"
                });

                kernel.UsePermission(permission);

                UpdateModel1(dataContext);

                Assert.Throws<AccessDeniedException>(() => { dataContext.SaveChanges(); });
            }
        }

        [Fact]
        public void EntityUpdate_CustomDataRole_NoAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePrincipal(GetCustomPrincipal());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = "Not_matching_state"
                });

                UpdateModel1(dataContext);

                Assert.Throws<AccessDeniedException>(() => { dataContext.SaveChanges(); });
            }
        }

        [Fact]
        public void EntityUpdate_CustomDataRole_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePrincipal(GetCustomPrincipal());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = null
                });

                UpdateModel1(dataContext);
                dataContext.SaveChanges(); ;
            }
        }

        [Fact]
        public void EntityUpdate_CustomDataRole_Status_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePrincipal(GetCustomPrincipal());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = Seed.CustomData
                });

                UpdateModel1(dataContext);
                dataContext.SaveChanges(); ;
            }
        }

        [Fact]
        public void EntityUpdate_CustomDataRole_Property_NoAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePrincipal(GetCustomPrincipal());

            using (var dataContext = InitDataContext(kernel))
            {
                var permission = new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = Seed.CustomData
                };

                permission.ColumnRestrictions.Add(new TestColumnRestriction()
                {
                    PropertyName = "Property1"
                });

                permission.ColumnRestrictions.Add(new TestColumnRestriction()
                {
                    PropertyName = "Property2"
                });

                kernel.UsePermission(permission);

                UpdateModel1(dataContext);

                Assert.Throws<AccessDeniedException>(() => { dataContext.SaveChanges(); });
            }
        }

        [Fact]
        public void EntityUpdate_Role_Row_HaveAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestRowPermission()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model1).Name,
                    RowIdentifier = "2"
                });

                UpdateModel1(dataContext, 2);
                dataContext.SaveChanges();;
            }
        }

        [Fact]
        public void EntityUpdate_CustomDataRole_Row_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePrincipal(GetCustomPrincipal());

            using (var dataContext = InitDataContext(kernel))
            {
                var permission = new TestRowPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = null,
                    RowIdentifier = "2"
                };

                kernel.UsePermission(permission);

                UpdateModel1(dataContext, 2);
                dataContext.SaveChanges();
            }
        }

        [Fact]
        public void EntityUpdate_CustomDataRole_Status_Row_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePrincipal(GetCustomPrincipal());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestRowPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = Seed.CustomData,
                    RowIdentifier = "2"
                });

                UpdateModel1(dataContext, 2);
                dataContext.SaveChanges();
            }
        }

        [Fact]
        public void EntityUpdate_Role_Row_Property_NoAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                var sysRolePermission = new TestRowPermission()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model1).Name,
                    RowIdentifier = "2"
                };

                sysRolePermission.ColumnRestrictions.Add(new TestColumnRestriction()
                {
                    PropertyName = "Property1"
                });

                sysRolePermission.ColumnRestrictions.Add(new TestColumnRestriction()
                {
                    PropertyName = "Property2"
                });

                kernel.UsePermission(sysRolePermission);

                UpdateModel1(dataContext, 2);

                Assert.Throws<AccessDeniedException>(() => { dataContext.SaveChanges(); });

            }
        }

        [Fact]
        public void EntityUpdate_CustomDataRole_Row_Property_NoAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePrincipal(GetCustomPrincipal());

            using (var dataContext = InitDataContext(kernel))
            {
                var wfRolePermissions = new TestRowPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = Seed.CustomData,
                    RowIdentifier = "2"
                };

                wfRolePermissions.ColumnRestrictions.Add(new TestColumnRestriction()
                {
                    PropertyName = "Property1"
                });

                wfRolePermissions.ColumnRestrictions.Add(new TestColumnRestriction()
                {
                    PropertyName = "Property2"
                });

                kernel.UsePermission(wfRolePermissions);

                UpdateModel1(dataContext, 2);

                Assert.Throws<AccessDeniedException>(() => { dataContext.SaveChanges(); });
            }
        }


        private void UpdateModel1(TestDbContext dataContext, int Model1Id = default(int))
        {
            Model1 Model1 = null;
            if (Model1Id == default(int))
            {
                Model1 = dataContext.Model1s.FirstOrDefault();
            }
            else
            {
                Model1 = dataContext.Model1s.Find(Model1Id);
            }

            Model1.Property1 = "UpdatedValue";
            Model1.Property2 = "222";
        }

        private CustomDbPrincipal GetCustomPrincipal()
        {
            var customDataPrincipal = new CustomDbPrincipal();
            customDataPrincipal.CustomCheckData = Seed.CustomData;
            return customDataPrincipal;
        }

        protected override void SeedDatabase(TestDbContext dataContext)
        {

            foreach (var Model1 in Seed.Model1s)
            {
                dataContext.Model1s.Add(Model1);
            }

            dataContext.SaveChanges();

        }

        private static class Seed
        {
            public static List<Model1> Model1s = new List<Model1>()
            {
                new Model1() { Property1 = "Model11", Property2 = "1" },
                new Model1() { Property1 = "Model12", Property2 = "2" },
                new Model1() { Property1 = "Model13", Property2 = "3" },
                new Model1() { Property1 = "Model14", Property2 = "4" },
            };

            public static string CustomData { get; set; } = "CustomData";
        }
    }
}
