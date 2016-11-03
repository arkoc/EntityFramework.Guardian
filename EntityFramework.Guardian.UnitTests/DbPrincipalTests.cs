using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.UnitTests.Models;
using System.Collections.Generic;
using Xunit;

namespace EntityFramework.Guardian.UnitTests
{

    public class DbPrincipalTests
    {
        private DbPrincipal _princpal;
        public DbPrincipalTests()
        {
            _princpal = new DbPrincipal();
            _princpal.GeneralPermissions.AddRange(Seed.GeneralPermissions);
            _princpal.RowLevelPermissions.AddRange(Seed.RowPermissions);
        }

        [Fact]
        public void GetGeneralPermissions_ShouldReturnEmpty()
        {
            var result = _princpal.GetGeneralPermissions(typeof(Model2).Name, AccessTypes.Get);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void GetGeneralPermissions_ShouldReturnResult()
        {
            var result = _princpal.GetGeneralPermissions(typeof(Model2).Name, AccessTypes.Delete);
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void GetRowPermissions_ShouldReturnEmpty1()
        {
            var result = _princpal.GetRowLevelPermissions(typeof(Model1).Name, AccessTypes.Get, "2");
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void GetRowPermissions_ShouldReturnEmpty2()
        {
            var result = _princpal.GetRowLevelPermissions(typeof(Model1).Name, AccessTypes.Update, "1");
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void GetRowPermissions_ShouldReturnResult()
        {
            var result = _princpal.GetRowLevelPermissions(typeof(Model2).Name, AccessTypes.Delete, "2");
            Assert.Equal(1, result.Count);
        }

        public static class Seed
        {
            public static List<IPermission> GeneralPermissions { get; set; } = new List<IPermission>()
             {
                new TestPermission()
                {
                    AccessType = AccessTypes.Get,
                    EntityTypeName = typeof(Model1).Name
                },
                new TestPermission()
                {
                    AccessType = AccessTypes.Add,
                    EntityTypeName = typeof(Model1).Name
                },
                new TestPermission()
                {
                    AccessType = AccessTypes.Delete,
                    EntityTypeName = typeof(Model2).Name
                },
                new TestPermission()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model2).Name
                }
            };
            public static List<IRowPermission> RowPermissions { get; set; } = new List<IRowPermission>()
             {
                new TestRowPermission()
                {
                    AccessType = AccessTypes.Get,
                    EntityTypeName = typeof(Model1).Name,
                    RowIdentifier = "1"
                },
                new TestRowPermission()
                {
                    AccessType = AccessTypes.Add,
                    EntityTypeName = typeof(Model1).Name,
                    RowIdentifier = "1"
                },
                new TestRowPermission()
                {
                    AccessType = AccessTypes.Delete,
                    EntityTypeName = typeof(Model2).Name,
                    RowIdentifier = "2"
                },
                new TestRowPermission()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model2).Name,
                    RowIdentifier = "2"
                }
            };
        }
    }
}
