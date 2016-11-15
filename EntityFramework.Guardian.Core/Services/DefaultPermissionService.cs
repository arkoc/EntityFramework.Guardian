// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EntityFramework.Guardian.Entities;

namespace EntityFramework.Guardian.Services
{
    /// <summary>
    /// Default implementation of IPermissionService
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Services.IPermissionService" />
    public class DefaultPermissionService : IPermissionService
    {
        /// <summary>
        /// Gets or sets the general permissions.
        /// </summary>
        /// <value>
        /// The general permissions.
        /// </value>
        public virtual List<IPermission> GeneralPermissions { get; set; } = new List<IPermission>();

        /// <summary>
        /// Gets or sets the row level permissions.
        /// </summary>
        /// <value>
        /// The row level permissions.
        /// </value>
        public virtual List<IRowPermission> RowLevelPermissions { get; set; } = new List<IRowPermission>();

        /// <summary>
        /// Adds the general permission.
        /// </summary>
        /// <param name="permission">The permission to add.</param>
        public virtual void AddGeneralPermission(IPermission permission)
        {
            GeneralPermissions.Add(permission);
        }

        /// <summary>
        /// Adds the row level permission.
        /// </summary>
        /// <param name="permission">The permission to add.</param>
        public virtual void AddRowLevelPermission(IRowPermission permission)
        {
            RowLevelPermissions.Add(permission);
        }

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
        /// <param name="accessType">Type of the access.</param>
        /// <param name="rowKey">The db row key of entity.</param>
        /// <returns></returns>
        public virtual List<IRowPermission> GetRowLevelPermissions(string entityTypeName, AccessTypes accessType, string rowKey)
        {
            return RowLevelPermissions
               .Where(x => x.EntityTypeName == entityTypeName && x.AccessType == accessType && x.RowIdentifier == rowKey)
               .ToList();
        }
    }
}
