// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Entities;
using System.Collections.Generic;

namespace EntityFramework.Guardian.Guards
{
    /// <summary>
    /// Default implementation of IQueryGuard which calls registered query policies.
    /// </summary>
    /// <seealso cref="IQueryGuard" />
    public class DefaultQueryGuard : IQueryGuard
    {
        /// <summary>
        /// Calls all registered query policies in GuardianKernel
        /// </summary>
        /// <param name="context">The protection context.</param>
        public void Protect(QueryProtectionContext context)
        {
            context.Entry.Entity.ProtectionResult = ProtectionResults.Allow;
            context.Entry.Entity.RestrictedProperties = new List<string>();

            foreach (var policy in context.Kernel.QueryPolicies)
            {

                var result = policy.Check(context);

                if (result.IsSuccess == false)
                {
                    context.Entry.Entity.ProtectionResult = ProtectionResults.Deny;
                    // If one of policies fail, we don't need to apply other ones
                    break;
                }

                context.Entry.Entity.RestrictedProperties.AddRange(result.RestrictedProperties);
            }
        }
    }
}
