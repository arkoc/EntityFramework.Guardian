// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace EntityFramework.Guardian.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public interface IColumnRestriction
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        string PropertyName { get; }
    }
}
