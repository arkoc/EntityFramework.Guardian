// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// </summary>
    public class QueryProtectionContext
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        /// <value>
        /// The kernel.
        /// </value>
        public GuardianKernel Kernel { get; set; }

        /// <summary>
        /// Gets or sets the entity type.
        /// </summary>
        /// <value>
        /// The name of the entity type.
        /// </value>
        public Type EntityType { get; set; }

        /// <summary>
        /// Gets or sets the entry.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public IObjectAccessEntry Entry { get; set; }
    }
}
