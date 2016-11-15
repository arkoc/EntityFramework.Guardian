// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Exceptions;
using EntityFramework.Guardian.Extensions;
using Xunit;
using EntityFramework.Guardian.Tests.Models;
using EntityFramework.Guardian.IntegrationTests.Database;

namespace EntityFramework.Guardian.IntegrationTests
{
    public class InsertItegrationTests : IntegrationTestsBase
    {
        [Fact]
        public void EntityInsert_Role_NoAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                AddModel1(dataContext);

                Assert.Throws<AccessDeniedException>(() => { dataContext.SaveChanges(); });
            }
        }

        [Fact]
        public void EntityInsert_Role_HaveAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestPermission()
                {
                    AccessType = AccessTypes.Add,
                    EntityTypeName = typeof(Model1).Name
                });

                AddModel1(dataContext);

                dataContext.SaveChanges(); ;
            }
        }

        [Fact]
        public void EntityInsert_Role_Property_NoAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
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

                AddModel1(dataContext);

                Assert.Throws<AccessDeniedException>(() => { dataContext.SaveChanges(); });
            }
        }

        [Fact]
        public void EntityInsert_CustomDataRole_NoAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Add,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = "Not_Matching_Data"
                });

                AddModel1(dataContext);

                Assert.Throws<AccessDeniedException>(() => { dataContext.SaveChanges(); });
            }
        }

        [Fact]
        public void EntityInsert_CustomDataRole_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Add,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = null
                });

                AddModel1(dataContext);

                dataContext.SaveChanges();
            }
        }

        [Fact]
        public void EntityInsert_CustomDataRole_Status_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UsePermission(new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Add,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = Seed.CustomData
                });

                AddModel1(dataContext);

                dataContext.SaveChanges(); ;
            }
        }

        [Fact]
        public void EntityInsert_CustomDataRole_Property_NoAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                var permission = new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Add,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = Seed.CustomData
                };

                permission.ColumnRestrictions.Add(new TestColumnRestriction()
                {
                    PropertyName = "Property1"
                });

                kernel.UsePermission(permission);

                AddModel1(dataContext);

                Assert.Throws<AccessDeniedException>(() => { dataContext.SaveChanges(); });
            }
        }

        private void AddModel1(TestDbContext dataContext)
        {
            dataContext.Model1s.Add(new Model1()
            {
                Property1 = "Name",
                Property2 = "Value"
            });
        }

        private CustomPermissionService GetCustomPermissionService()
        {
            var customPermissionService = new CustomPermissionService();
            customPermissionService.CustomCheckData = Seed.CustomData;
            return customPermissionService;
        }

        protected override void SeedDatabase(TestDbContext dataContext)
        {
        }

        private static class Seed
        {
            public static string CustomData { get; set; } = "CustomData";
        }
    }
}
