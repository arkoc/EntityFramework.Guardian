using EntityFramework.Guardian.Core;
using EntityFramework.Guardian.Core.Models;
using EntityFramework.Guardian.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Tests
{
    [TestClass]
    public class GuardianPermissionsTests
    {
        private GuardianPermissions _permissions;
        public GuardianPermissionsTests()
        {
            _permissions = new GuardianPermissions();
            _permissions.General.AddRange(Seed.GeneralPermissions);
            _permissions.RowLevel.AddRange(Seed.RowPermissions);
            _permissions.ColumnLevel.AddRange(Seed.ColumnRestrictions);
        }

        [TestMethod]
        public void GetGeneralPermissions_ShouldReturnEmpty()
        {
            var result = _permissions.GetGeneralPermissions(typeof(Model2).Name, AccessTypes.Get);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetGeneralPermissions_ShouldReturnResult()
        {
            var result = _permissions.GetGeneralPermissions(typeof(Model2).Name, AccessTypes.Delete);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetRowPermissions_ShouldReturnEmpty1()
        {
            var result = _permissions.GetRowLevelPermissions(typeof(Model1).Name, "2", AccessTypes.Get);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetRowPermissions_ShouldReturnEmpty2()
        {
            var result = _permissions.GetRowLevelPermissions(typeof(Model1).Name, "1", AccessTypes.Update);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetRowPermissions_ShouldReturnResult()
        {
            var result = _permissions.GetRowLevelPermissions(typeof(Model2).Name, "2", AccessTypes.Delete);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetColumnRestrictions_ShouldReturnEmpty()
        {
            var result = _permissions.GetColumnLevelRestrictions(typeof(Model1).Name, AccessTypes.Update);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetColumnRestrictions_ShouldReturnResult()
        {
            var result = _permissions.GetColumnLevelRestrictions(typeof(Model2).Name, AccessTypes.Update);
            Assert.AreEqual(1, result.Count);
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
            public static List<IColumnRestriction> ColumnRestrictions { get; set; } = new List<IColumnRestriction>()
             {
                new TestColumnRestriction()
                {
                    AccessType = AccessTypes.Get,
                    EntityTypeName = typeof(Model1).Name,
                    PropertyName = "Property1"
                },
                new TestColumnRestriction()
                {
                    AccessType = AccessTypes.Add,
                    EntityTypeName = typeof(Model1).Name,
                    PropertyName = "Property1"
                },
                new TestColumnRestriction()
                {
                    AccessType = AccessTypes.Delete,
                    EntityTypeName = typeof(Model2).Name,
                    PropertyName = "Property1"
                },
                new TestColumnRestriction()
                {
                    AccessType = AccessTypes.Update,
                    EntityTypeName = typeof(Model2).Name,
                    PropertyName = "Property1"
                }
            };
        }
    }
}
