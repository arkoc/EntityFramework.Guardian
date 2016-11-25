// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Submit Policy interface
    /// </summary>
    public interface ISubmitPolicy
    {
        /// <summary>
        /// Checks the policy by specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><see cref="SubmitPolicyResult"/> of policy checking.</returns>
        SubmitPolicyResult Check(SubmitProtectionContext context);
    }
}
