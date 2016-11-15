﻿using EntityFramework.Guardian.Entities;
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
        /// Uses the permission.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <param name="permission">The permission to add in DbPrincipal.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">permission</exception>
        public static GuardianKernel UsePermission(this GuardianKernel kernel, IPermission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            kernel.Services.PermissionService.AddGeneralPermission(permission);

            return kernel;
        }

        /// <summary>
        /// Uses the permission.
        /// </summary>
        /// <param name="kernel">The guardian kernel.</param>
        /// <param name="permission">The permission to add in DbPrincipal.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">permission</exception>
        public static GuardianKernel UsePermission(this GuardianKernel kernel, IRowPermission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            kernel.Services.PermissionService.AddRowLevelPermission(permission);

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

            kernel.Services.PermissionService = service;

            return kernel;
        }
    }
}
