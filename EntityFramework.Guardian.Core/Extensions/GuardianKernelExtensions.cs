using EntityFramework.Guardian.Entities;
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

            kernel.Principal.GeneralPermissions.Add(permission);

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

            kernel.Principal.RowLevelPermissions.Add(permission);

            return kernel;
        }


        /// <summary>
        /// Uses the principal.
        /// </summary>
        /// <param name="kernel">The guardian kernel.</param>
        /// <param name="principal">The principal to use in kernel.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">principal</exception>
        public static GuardianKernel UsePrincipal(this GuardianKernel kernel, IDbPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            kernel.Principal = principal;

            return kernel;
        }
    }
}
