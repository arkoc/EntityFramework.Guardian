// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Guards;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// Guardian Kernel Guards
    /// </summary>
    public class KernelGuards
    {
        /// <summary>
        /// Gets the modify protector.
        /// </summary>
        /// <value>
        /// The modify protector.
        /// </value>
        public IModifyGuard ModifyGuard { get; set; }

        /// <summary>
        /// Gets the retrieve protector.
        /// </summary>
        /// <value>
        /// The retrieve protector.
        /// </value>
        public IRetrieveGuard RetrieveGuard { get; set; }
    }
}
