using EntityFramework.Guardian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// 
    /// </summary>
    public class GuardianPermissions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuardianPermissions"/> class.
        /// </summary>
        public GuardianPermissions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuardianPermissions"/> class.
        /// </summary>
        /// <param name="generalPermissions">The general permissions.</param>
        /// <param name="rowLevelPermissions">The row level permissions.</param>
        /// <param name="columnLevelRestrictions">The column level restrictions.</param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
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

        /// <summary>
        /// Gets the general permissions.
        /// </summary>
        /// <value>
        /// The general permissions.
        /// </value>
        public List<IPermission> General { get; private set; } = new List<IPermission>();

        /// <summary>
        /// Gets the row level permissions.
        /// </summary>
        /// <value>
        /// The row level permissions.
        /// </value>
        public List<IRowPermission> RowLevel { get; private set; } = new List<IRowPermission>();

        /// <summary>
        /// Gets the column level restrictions.
        /// </summary>
        /// <value>
        /// The column level restrictions.
        /// </value>
        public List<IColumnRestriction> ColumnLevel { get; private set; } = new List<IColumnRestriction>();

        /// <summary>
        /// Gets the general permissions.
        /// </summary>
        /// <param name="entityTypeName">Name of the entity type.</param>
        /// <param name="accessType">Type of the access.</param>
        /// <returns></returns>
        public virtual List<IPermission> GetGeneralPermissions(string entityTypeName, AccessTypes accessType)
        {
            return General
                .Where(x => x.EntityTypeName == entityTypeName && x.AccessType == accessType)
                .ToList();
        }

        /// <summary>
        /// Gets the row level permissions.
        /// </summary>
        /// <param name="entityTypeName">Name of the entity type.</param>
        /// <param name="key">The row key of entity.</param>
        /// <param name="accessType">Type of the access.</param>
        /// <returns></returns>
        public virtual List<IRowPermission> GetRowLevelPermissions(string entityTypeName, string key, AccessTypes accessType)
        {
            return RowLevel
                .Where(x => x.EntityTypeName == entityTypeName && x.AccessType == accessType && x.RowIdentifier == key)
                .ToList();
        }

        /// <summary>
        /// Gets the column level restrictions.
        /// </summary>
        /// <param name="entityTypeName">Name of the entity type.</param>
        /// <param name="accessType">Type of the access.</param>
        /// <returns></returns>
        public virtual List<IColumnRestriction> GetColumnLevelRestrictions(string entityTypeName, AccessTypes accessType)
        {
            return ColumnLevel
                .Where(x => x.EntityTypeName == entityTypeName && x.AccessType == accessType)
                .ToList();
        }
    }
}
