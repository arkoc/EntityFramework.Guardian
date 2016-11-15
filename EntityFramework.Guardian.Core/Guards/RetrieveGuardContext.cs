// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace EntityFramework.Guardian.Guards
{
    /// <summary>
    /// Context for Retrieve Guard
    /// </summary>
    public class RetrieveGuardContext
    {
        /// <summary>
        /// Gets or sets the entry.
        /// </summary>
        /// <value>
        /// The entry.
        /// </value>
        public IObjectAccessEntry Entry { get; set; }
    }
}
