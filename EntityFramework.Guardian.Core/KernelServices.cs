// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Services;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// Guardian Kernel Services
    /// </summary>
    public class KernelServices
    {
        /// <summary>
        /// Gets or sets the permission service.
        /// </summary>
        /// <value>
        /// The permission service.
        /// </value>
        public IPermissionService PermissionService { get; set; }

        /// <summary>
        /// Gets or sets the entity key provider.
        /// </summary>
        /// <value>
        /// The entity key provider.
        /// </value>
        public IEntityKeyProvider EntityKeyProvider { get; set; }
    }
}
