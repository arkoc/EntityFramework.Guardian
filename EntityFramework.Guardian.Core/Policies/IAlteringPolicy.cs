// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Altering Policy interface
    /// </summary>
    public interface IAlteringPolicy
    {
        /// <summary>
        /// Checks the policy by specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><see cref="AlteringPolicyResult"/> of policy checking.</returns>
        AlteringPolicyResult Check(AlteringPolicyContext context);
    }
}
