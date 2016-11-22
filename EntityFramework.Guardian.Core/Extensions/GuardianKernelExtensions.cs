// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Services;
using System;

namespace EntityFramework.Guardian.Extensions
{
    /// <summary>
    /// <see cref="GuardianKernel"/> extensions
    /// </summary>
    public static class GuardianKernelExtensions
    {
        /// <summary>
        /// Uses the permission in InMemmoryPermissionService.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <param name="permission">The permission to add in DbPrincipal.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">permission</exception>
        public static GuardianKernel UseInMemoryPermission(this GuardianKernel kernel, IPermission permission)
        {
            if (kernel.PermissionService is InMemoryPermissionService == false)
            {
                throw new InvalidOperationException("Permission service must be InMemmoryPermissionService to use this method");
            }

            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            (kernel.PermissionService as InMemoryPermissionService).GeneralPermissions.Add(permission);

            return kernel;
        }

        /// <summary>
        /// Uses the permission in InMemmoryPermissionService.
        /// </summary>
        /// <param name="kernel">The guardian kernel.</param>
        /// <param name="permission">The permission to add in DbPrincipal.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">permission</exception>
        public static GuardianKernel UseInMemoryPermission(this GuardianKernel kernel, IRowPermission permission)
        {
            if (kernel.PermissionService is InMemoryPermissionService == false)
            {
                throw new InvalidOperationException("Permission service must be InMemmoryPermissionService to use this method");
            }

            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            (kernel.PermissionService as InMemoryPermissionService).RowLevelPermissions.Add(permission);

            return kernel;
        }


        /// <summary>
        /// Uses the permission service.
        /// </summary>
        /// <param name="kernel">The guardian kernel.</param>
        /// <param name="service">The permission service.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">principal</exception>
        public static GuardianKernel UsePermissionService(this GuardianKernel kernel, IPermissionService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            kernel.PermissionService = service;

            return kernel;
        }
    }
}
