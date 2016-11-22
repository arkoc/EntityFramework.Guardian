// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace EntityFramework.Guardian.Guards
{
    /// <summary>
    /// Retrieval Guard Interface
    /// </summary>
    public interface IRetrievalGuard
    {
        /// <summary>
        /// Protects by the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        void Protect(RetrievalGuardContext context);
    }
}
