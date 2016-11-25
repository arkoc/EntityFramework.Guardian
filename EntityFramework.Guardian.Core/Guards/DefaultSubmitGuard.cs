// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Exceptions;

namespace EntityFramework.Guardian.Guards
{
    /// <summary>
    /// Default implementation of IAlteringGuard which calls registered submit policies.
    /// </summary>
    /// <seealso cref="ISubmitGuard" />
    public class DefaultSubmitGuard : ISubmitGuard
    {
        /// <summary>
        /// Calls all registered submit policies in GuardianKernel
        /// </summary>
        /// <param name="context">The protection context.</param>
        /// <exception cref="AccessDeniedException"></exception>
        public void Protect(SubmitProtectionContext context)
        {
            foreach (var policy in context.Kernel.SubmitPolicies)
            {
                var result = policy.Check(context);

                if (result.IsSuccess == false)
                {
                    // If one of policies fail, we don't need to apply another ones
                    throw new AccessDeniedException();
                }
            }
        }
    }
}
