// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Retrieveal Policy interface
    /// </summary>
    public interface IRetrievalPolicy
    {
        /// <summary>
        /// Checks the policy by specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><see cref="RetrievalPolicyResult"/> of policy checking.</returns>
        RetrievalPolicyResult Check(RetrievalPolicyContext context);
    }
}
