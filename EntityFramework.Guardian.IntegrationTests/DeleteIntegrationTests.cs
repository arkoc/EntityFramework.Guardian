// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using System.Collections.Generic;
using Xunit;
using EntityFramework.Guardian.Exceptions;
using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Extensions;
using EntityFramework.Guardian.Tests.Models;
using EntityFramework.Guardian.IntegrationTests.Database;

namespace EntityFramework.Guardian.IntegrationTests
{
    public class DeleteIntegrationTests : IntegrationTestsBase
    {
        [Fact]
        public void EntityDelete_Role_NoAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                DeleteModel1(dataContext);
                Assert.Throws<AccessDeniedException>(() => { dataContext.SaveChanges(); });
            }
        }

        [Fact]
        public void EntityDelete_Role_HaveAccess()
        {
            var kernel = new GuardianKernel();
            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UseInMemoryPermission(new TestPermission()
                {
                    AccessType = AccessTypes.Delete,
                    EntityTypeName = typeof(Model1).Name
                });

                DeleteModel1(dataContext);

                dataContext.SaveChanges();
            }
        }


        [Fact]
        public void EntityDelete_CustomDataRole_NoAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UseInMemoryPermission(new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Delete,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = "Not_Matching_Data"
                });

                DeleteModel1(dataContext);

                Assert.Throws<AccessDeniedException>(() => { dataContext.SaveChanges(); });
            }
        }

        [Fact]
        public void EntityDelete_CustomDataRole_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UseInMemoryPermission(new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Delete,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = null
                });

                DeleteModel1(dataContext);
                dataContext.SaveChanges();
            }
        }

        [Fact]
        public void EntityDelete_CustomDataRole_Status_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UseInMemoryPermission(new TestPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Delete,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = Seed.CustomData
                });

                DeleteModel1(dataContext);
                dataContext.SaveChanges();
            }
        }


        [Fact]
        public void EntityDelete_Role_Row_HaveAccess()
        {
            var kernel = new GuardianKernel();

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UseInMemoryPermission(new TestRowPermission()
                {
                    AccessType = AccessTypes.Delete,
                    EntityTypeName = typeof(Model1).Name,
                    RowIdentifier = "2"
                });

                DeleteModel1(dataContext, 2);
                dataContext.SaveChanges();
            }
        }

        [Fact]
        public void EntityDelete_CustomDataRole_Row_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UseInMemoryPermission(new TestRowPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Delete,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = null,
                    RowIdentifier = "2"
                });

                DeleteModel1(dataContext, 2);
                dataContext.SaveChanges();
            }
        }

        [Fact]
        public void EntityDelete_CustomDataRole_Status_Row_HaveAccess()
        {
            var kernel = new GuardianKernel();
            kernel.UsePermissionService(GetCustomPermissionService());

            using (var dataContext = InitDataContext(kernel))
            {
                kernel.UseInMemoryPermission(new TestRowPermissionWithCustomField()
                {
                    AccessType = AccessTypes.Delete,
                    EntityTypeName = typeof(Model1).Name,
                    CustomField = Seed.CustomData,
                    RowIdentifier = "2"
                });

                DeleteModel1(dataContext, 2);
                dataContext.SaveChanges();
            }
        }

        private void DeleteModel1(TestDbContext dataContext, int Model1Id = default(int))
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

            dataContext.Model1s.Remove(Model1);
        }

        private CustomPermissionService GetCustomPermissionService()
        {
            var customPermissionService = new CustomPermissionService();
            customPermissionService.CustomCheckData = Seed.CustomData;
            return customPermissionService;
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
                new Model1() { Property1 = "Model12", Property2 = "2" }
            };

            public static string CustomData { get; set; } = "CustomData";
        }
    }
}
