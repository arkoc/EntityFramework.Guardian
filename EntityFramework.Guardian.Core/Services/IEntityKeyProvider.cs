// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace EntityFramework.Guardian.Services
{
    /// <summary>
    /// Interface that is used by <see cref="GuardianKernel"/> to gather row key of entity 
    /// </summary>
    public interface IEntityKeyProvider
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Entity row key.</returns>
        string GetKey(object entity);
    }
}
