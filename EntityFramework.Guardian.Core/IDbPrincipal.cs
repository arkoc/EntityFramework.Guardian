using EntityFramework.Guardian.Entities;
using System.Collections.Generic;

namespace EntityFramework.Guardian
{
    public interface IDbPrincipal
    {
        /// <summary>
        /// Gets the general permissions.
        /// </summary>
        /// <value>
        /// The general permissions.
        /// </value>
        List<IPermission> GeneralPermissions { get; }

        /// <summary>
        /// Gets the row level permissions.
        /// </summary>
        /// <value>
        /// The row level permissions.
        /// </value>
        List<IRowPermission> RowLevelPermissions { get; }

        /// <summary>
        /// Gets the column level restrictions.
        /// </summary>
        /// <value>
        /// The column level restrictions.
        /// </value>
        List<IColumnRestriction> ColumnLevelRestrictions { get; }

        /// <summary>
        /// Gets the general permissions.
        /// </summary>
        /// <param name="entityTypeName">Name of the entity type.</param>
        /// <param name="accessType">Type of the access.</param>
        /// <returns></returns>
        List<IPermission> GetGeneralPermissions(string entityTypeName, AccessTypes accessType);

        /// <summary>
        /// Gets the row level permissions.
        /// </summary>
        /// <param name="entityTypeName">Name of the entity type.</param>
        /// <param name="accessType">Type of the access.</param>
        /// <param name="key">The db row key of entity.</param>
        /// <returns></returns>
        List<IRowPermission> GetRowLevelPermissions(string entityTypeName, AccessTypes accessType, string key);

        /// <summary>
        /// Gets the column level restrictions.
        /// </summary>
        /// <param name="entityTypeName">Name of the entity type.</param>
        /// <param name="accessType">Type of the access.</param>
        /// <returns></returns>
        List<IColumnRestriction> GetColumnLevelRestrictions(string entityTypeName, AccessTypes accessType);
    }
}
