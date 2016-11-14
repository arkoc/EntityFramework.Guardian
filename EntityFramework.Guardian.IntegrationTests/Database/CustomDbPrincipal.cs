using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.IntegrationTests.Database
{
    public class CustomDbPrincipal : DbPrincipal
    {
        public string CustomCheckData { get; set; }

        public override List<IPermission> GetGeneralPermissions(string entityTypeName, AccessTypes accessType)
        {
            var generalPermissions = base.GetGeneralPermissions(entityTypeName, accessType);
            var filteredPermissions = generalPermissions
                .Where(x =>
                (x is TestPermissionWithCustomField) == false || 
                ((x as TestPermissionWithCustomField).CustomField == CustomCheckData || (x as TestPermissionWithCustomField).CustomField == null))
                .ToList();

            return filteredPermissions;
        }

        public override List<IRowPermission> GetRowLevelPermissions(string entityTypeName, AccessTypes accessType, string key)
        {
            var rowLevelPermissions = base.GetRowLevelPermissions(entityTypeName, accessType, key);

            var filteredPermissions = rowLevelPermissions
                 .Where(x =>
                 (x is TestRowPermissionWithCustomField) == false ||
                 ((x as TestRowPermissionWithCustomField).CustomField == CustomCheckData || (x as TestRowPermissionWithCustomField).CustomField == null))
                 .ToList();

            return filteredPermissions;
        }
    }
}
