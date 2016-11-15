// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Retrieve Policy interface
    /// </summary>
    public interface IRetrievePolicy
    {
        /// <summary>
        /// Checks the policy by specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><see cref="RetrievePolicyResult"/> of policy checking.</returns>
        RetrievePolicyResult Check(RetrievePolicyContext context);
    }
}
