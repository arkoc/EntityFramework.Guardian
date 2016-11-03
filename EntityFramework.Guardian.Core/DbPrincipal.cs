using EntityFramework.Guardian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework.Guardian
{
    public class DbPrincipal : IDbPrincipal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbPrincipal"/> class.
        /// </summary>
        public DbPrincipal()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbPrincipal"/> class.
        /// </summary>
        /// <param name="generalPermissions">The general permissions.</param>
        /// <param name="rowLevelPermissions">The row level permissions.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public DbPrincipal(
            List<IPermission> generalPermissions,
            List<IRowPermission> rowLevelPermissions)
        {

            if (generalPermissions == null)
            {
                throw new ArgumentNullException(nameof(generalPermissions));
            }

            if (rowLevelPermissions == null)
            {
                throw new ArgumentNullException(nameof(rowLevelPermissions));
            }

            GeneralPermissions = generalPermissions;
            RowLevelPermissions = rowLevelPermissions;
        }

        /// <summary>
        /// Gets the general permissions.
        /// </summary>
        /// <value>
        /// The general permissions.
        /// </value>
        public List<IPermission> GeneralPermissions { get; set; } = new List<IPermission>();

        /// <summary>
        /// Gets the row level permissions.
        /// </summary>
        /// <value>
        /// The row level permissions.
        /// </value>
        public List<IRowPermission> RowLevelPermissions { get; private set; } = new List<IRowPermission>();

        /// <summary>
        /// Gets the general permissions.
        /// </summary>
        /// <param name="entityTypeName">Name of the entity type.</param>
        /// <param name="accessType">Type of the access.</param>
        /// <returns></returns>
        public virtual List<IPermission> GetGeneralPermissions(string entityTypeName, AccessTypes accessType)
        {
            return GeneralPermissions
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
        public virtual List<IRowPermission> GetRowLevelPermissions(string entityTypeName, AccessTypes accessType, string key)
        {
            return RowLevelPermissions
                .Where(x => x.EntityTypeName == entityTypeName && x.AccessType == accessType && x.RowIdentifier == key)
                .ToList();
        }
    }
}
