// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Entities;
using System.Collections.Generic;

namespace EntityFramework.Guardian.Services
{
    /// <summary>
    /// Permission service to retrive information about current security context
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        /// Adds the general permission.
        /// </summary>
        /// <param name="permission">The permission to add.</param>
        void AddGeneralPermission(IPermission permission);

        /// <summary>
        /// Adds the row level permission.
        /// </summary>
        /// <param name="permission">The permission to add.</param>
        void AddRowLevelPermission(IRowPermission permission);

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
        /// <param name="rowKey">The db row key of entity.</param>
        /// <returns></returns>
        List<IRowPermission> GetRowLevelPermissions(string entityTypeName, AccessTypes accessType, string rowKey);
    }
}
