// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// </summary>
    public class SubmitProtectionContext
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        /// <value>
        /// The kernel.
        /// </value>
        public GuardianKernel Kernel { get; set; }

        /// <summary>
        /// Gets or sets the type of  entity
        /// </summary>
        /// <value>
        /// The name of the entity type.
        /// </value>
        public Type EntityType { get; set; }

        /// <summary>
        /// Gets or sets the type of the action.
        /// </summary>
        /// <value>
        /// The type of the access.
        /// </value>
        public IObjectAccessEntry Entry { get; set; }

        /// <summary>
        /// Changed values for properties that have been changed
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public IReadOnlyDictionary<string, object> LocalValues { get; set; }

        /// <summary>
        /// OriginalValues for properties that have been changed
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public IReadOnlyDictionary<string, object> OriginalValues { get; set; }
    }
}
