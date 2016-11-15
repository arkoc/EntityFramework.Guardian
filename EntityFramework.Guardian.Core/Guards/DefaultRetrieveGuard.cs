// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Policies;
using System;
using System.Collections.Generic;

namespace EntityFramework.Guardian.Guards
{
    /// <summary>
    /// Default implementation of IRetrieveGuard which calss registered retrieve policies.
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Guards.IRetrieveGuard" />
    public class DefaultRetrieveGuard : IRetrieveGuard
    {
        private readonly GuardianKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRetrieveGuard"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <exception cref="System.ArgumentNullException">kernel</exception>
        public DefaultRetrieveGuard(GuardianKernel kernel)
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
        public void Protect(RetrieveGuardContext context)
        {
            context.Entry.Entity.ProtectionResult = ProtectionResults.Allow;
            context.Entry.Entity.ProtectedProperties = new List<string>();

            foreach (var policy in _kernel.Policies.RetrievePolicies)
            {
                var policyContext = RetrievePolicyContext.For(_kernel, context.Entry);

                var result = policy.Check(policyContext);

                if (result.IsSuccess == false)
                {
                    context.Entry.Entity.ProtectionResult = ProtectionResults.Deny;
                    // If one of policies fail, we don't nned to apply other ones
                    break;
                }

                context.Entry.Entity.ProtectedProperties.AddRange(result.RestrictedProperties);
            }
        }
    }
}
