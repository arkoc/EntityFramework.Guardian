using EntityFramework.Guardian.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core
{
    public class GuardianPermissions
    {
        public GuardianPermissions()
        {
        }

        public GuardianPermissions(
            List<IPermission> generalPermissions,
            List<IRowPermission> rowLevelPermissions,
            List<IColumnRestriction> columnLevelRestrictions)
        {

            if (generalPermissions == null)
            {
                throw new ArgumentNullException(nameof(generalPermissions));
            }

            if (rowLevelPermissions == null)
            {
                throw new ArgumentNullException(nameof(rowLevelPermissions));
            }

            if (columnLevelRestrictions == null)
            {
                throw new ArgumentNullException(nameof(columnLevelRestrictions));
            }

            General = generalPermissions;
            RowLevel = rowLevelPermissions;
            ColumnLevel = columnLevelRestrictions;
        }

        public List<IPermission> General { get; private set; } = new List<IPermission>();
        public List<IRowPermission> RowLevel { get; private set; } = new List<IRowPermission>();
        public List<IColumnRestriction> ColumnLevel { get; private set; } = new List<IColumnRestriction>();

        public List<IPermission> GetGeneralPermission(string entityTypeName, AccessTypes accessType)
        {
            return General
                .Where(x => x.EntityTypeName == entityTypeName && x.AccessType == accessType)
                .ToList();
        }

        public List<IRowPermission> GetRowLevelPermission(string entityTypeName, string key, AccessTypes accessType)
        {
            return RowLevel
                .Where(x => x.EntityTypeName == entityTypeName && x.AccessType == accessType && x.RowIdentifier == key)
                .ToList();
        }

        public List<IColumnRestriction> GetColumnLevelRestrictions(string entityTypeName, AccessTypes accessType)
        {
            return ColumnLevel
                .Where(x => x.EntityTypeName == entityTypeName && x.AccessType == accessType)
                .ToList();
        }
    }
}
