// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Exceptions;
using EntityFramework.Guardian.Policies;
using System;

namespace EntityFramework.Guardian.Guards
{
    /// <summary>
    /// Default implementation of IAlteringGuard which calls all registered altering policies
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Guards.IAlteringGuard" />
    public class DefaultAlteringGuard : IAlteringGuard
    {
        private readonly GuardianKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultAlteringGuard"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <exception cref="System.ArgumentNullException">kernel</exception>
        public DefaultAlteringGuard(GuardianKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }

            _kernel = kernel;
        }

        /// <summary>
        /// Protects by the specified context.
        /// </summary>
        /// <param name="context">The protection context.</param>
        /// <exception cref="EntityFramework.Guardian.Exceptions.AccessDeniedException"></exception>
        public void Protect(AlteringGuardContext context)
        {
            foreach (var policy in _kernel.ModifyPolicies)
            {
                var policyContext = AlteringPolicyContext.For(
                        _kernel,
                        context.Entry,
                        context.AffectedProperties
                    );

                var result = policy.Check(policyContext);

                if (result.IsSuccess == false)
                {
                    // If one of policies fail, we don't need to apply another ones
                    throw new AccessDeniedException();
                }
            }
        }
    }
}
