using System.Collections.Generic;
using System.Linq;
using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Extensions;
using Xunit;
using EntityFramework.Guardian.Tests.Models;
using EntityFramework.Guardian.IntegrationTests.Database;

namespace EntityFramework.Guardian.IntegrationTests
{
    public class GetIntegrationTests : IntegrationTestsBase
    {
        [Fact]
        public void EntityGet_Role_NoAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                List<Model1> Model1s = dataContext.Model1s.ToList().Protect();

                var count = Model1s.Count;

                Assert.Equal(0, count);
            }
        }

        [Fact]
        public void EntityGet_Role_HaveAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestPermission()
                {
                    AccessType = AccessTypes.Get,
                    EntityTypeName = typeof(Model1).Name
                });

                List<Model1> Model1s = dataContext.Model1s.ToList().Protect();

                var count = Model1s.Count;

                Assert.Equal(Seed.Model1s.Count, count);
            }
        }

        [Fact]
        public void EntityGet_Role_Property_NoAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                var permission = new TestPermission()
                {
                    AccessType = AccessTypes.Get,
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

                List<Model1> Model1s = dataContext.Model1s.ToList().Protect();

                foreach (var Model1 in Model1s)
                {
                    Assert.Equal(null, Model1.Property1);
                    Assert.Equal(null, Model1.Property2);
                }
            }
        }

        [Fact]
        public void EntityGet_CustomDataRole_NoAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Get,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = "Not_matching_data"
                });

                List<Model1> Model1s = dataContext.Model1s.ToList().Protect();

                var count = Model1s.Count;

                Assert.Equal(0, count);
            }
        }

        [Fact]
        public void EntityGet_CustomDataRole_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Get,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = null
                });

                List<Model1> Model1s = dataContext.Model1s.ToList().Protect();

                var count = Model1s.Count;

                Assert.Equal(Seed.Model1s.Count, count);
            }
        }

        [Fact]
        public void EntityGet_CustomDataRole_Status_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Get,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = Seed.CustomData
                });


                List<Model1> Model1s = dataContext.Model1s.ToList().Protect();

                var count = Model1s.Count;

                Assert.Equal(Seed.Model1s.Count, count);
            }
        }

        [Fact]
        public void EntityGet_CustomDataRole_Property_NoAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                var permission = new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Get,
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

                List<Model1> Model1s = dataContext.Model1s.ToList().Protect();

                foreach (var Model1 in Model1s)
                {
                    Assert.Equal(null, Model1.Property1);
                    Assert.Equal(null, Model1.Property2);
                }
            }
        }

        [Fact]
        public void EntityGet_Role_Row_HaveAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestRowPermission()
                {
                    AccessType = AccessTypes.Get,
                    EntityTypeName = typeof(Model1).Name,
                    RowIdentifier = "1"
                });

                List<Model1> Model1s = dataContext.Model1s.ToList().Protect();

                var count = Model1s.Count;

                Assert.Equal(1, count);

                var model1 = Model1s.FirstOrDefault().Protect();

                Assert.Equal(1, model1.Id);
            }
        }

        [Fact]
        public void EntityGet_CustomDataRole_Row_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestRowPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Get,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = null,
                    RowIdentifier = "1"
                });

                List<Model1> Model1s = dataContext.Model1s.ToList().Protect();

                var count = Model1s.Count;

                Assert.Equal(1, count);

                var model1val = Model1s.FirstOrDefault();

                Assert.Equal(1, model1val.Id);
            }
        }

        [Fact]
        public void EntityGet_CustomDataRole_Status_Row_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestRowPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Get,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = Seed.CustomData,
                    RowIdentifier = "1"
                });

                List<Model1> Model1s = dataContext.Model1s.ToList().Protect();

                var count = Model1s.Count;

                Assert.Equal(1, count);

                var model1val = Model1s.FirstOrDefault();

                Assert.Equal(1, model1val.Id);
            }
        }

        [Fact]
        public void EntityGet_Role_Row_Property_NoAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                var permission = new TestRowPermission()
                {
                    AccessType = AccessTypes.Get,
                    EntityTypeName = typeof(Model1).Name,
                    RowIdentifier = "1"
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

                var model1val = dataContext.Model1s.FirstOrDefault().Protect();

                Assert.Equal(null, model1val.Property1);
                Assert.Equal(null, model1val.Property1);

            }
        }

        [Fact]
        public void EntityGet_CustomDataRole_Row_Property_NoAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                var permission = new TestRowPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Get,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = Seed.CustomData,
                    RowIdentifier = "1"
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

                var model1val = dataContext.Model1s.FirstOrDefault().Protect();

                Assert.Equal(null, model1val.Property1);
                Assert.Equal(null, model1val.Property2);
            }
        }

        private CustomPermissionService GetCustomPermissionService()
        {
            var customPermissionService = new CustomPermissionService();
            customPermissionService.CustomCheckData = Seed.CustomData;
            return customPermissionService;
        }

        protected override void SeedDatabase(TestDbContext dataContext)
        {
            foreach(var Model1 in Seed.Model1s)
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
                new Model1() { Property1 = "Model14", Property2 = "4" }
            };

            public static string CustomData { get; set; } = "CustomData";
        }
    }


}
