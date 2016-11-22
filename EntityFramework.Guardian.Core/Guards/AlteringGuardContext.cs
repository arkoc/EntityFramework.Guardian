// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace EntityFramework.Guardian.Guards
{
    /// <summary>
    /// Context for Modify Guard
    /// </summary>
    public class AlteringGuardContext
    {
        /// <summary>
        /// Gets or sets the entry.
        /// </summary>
        /// <value>
        /// The entry.
        /// </value>
        public IObjectAccessEntry Entry { get; set; }

        /// <summary>
        /// Gets or sets the affected properties.
        /// </summary>
        /// <value>
        /// The affected properties.
        /// </value>
        public List<string> AffectedProperties { get; set; } = new List<string>();
    }
}
