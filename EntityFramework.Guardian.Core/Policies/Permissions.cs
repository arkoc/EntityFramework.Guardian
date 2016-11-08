using EntityFramework.Guardian.Entities;
using System.Collections.Generic;

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// 
    /// </summary>
    public class Permissions
    {
        /// <summary>
        /// Gets or sets the general permissions.
        /// </summary>
        /// <value>
        /// The general permissions.
        /// </value>
        public List<IPermission> GeneralPermissions { get; set; } = new List<IPermission>();

        /// <summary>
        /// Gets or sets the row level permissions.
        /// </summary>
        /// <value>
        /// The row level permissions.
        /// </value>
        public List<IRowPermission> RowLevelPermissions { get; set; } = new List<IRowPermission>();

        /// <summary>
        /// Gets or sets the column restrictions.
        /// </summary>
        /// <value>
        /// The column restrictions.
        /// </value>
        public List<IColumnRestriction> ColumnRestrictions { get; set; } = new List<IColumnRestriction>();
    }
}
