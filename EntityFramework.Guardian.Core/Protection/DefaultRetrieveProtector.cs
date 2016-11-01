using EntityFramework.Guardian.Policies;
using System;
using System.Collections.Generic;

namespace EntityFramework.Guardian.Protection
{
    /// <summary>
    /// Default implementation of IRetrieveProtector that protects by registered policies.
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Protection.IRetrieveProtector" />
    public class DefaultRetrieveProtector : IRetrieveProtector
    {
        private readonly GuardianKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRetrieveProtector"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <exception cref="System.ArgumentNullException">kernel</exception>
        public DefaultRetrieveProtector(GuardianKernel kernel)
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
        public void Protect(RetrieveProtectionContext context)
        {
            foreach (var policy in _kernel.RetrieveProtectionPolicies)
            {
                var policyContext = new RetrievePolicyContext()
                {
                    Entity = context.Entry.Entity,
                    EntityRowKey = _kernel.EntityKeyProvider.GetKey(context.Entry.Entity),
                    EntityTypeName = context.Entry.Entity.GetType().Name,
                };

                context.Entry.Entity.ProtectionResult = Models.ProtectionResults.Allow;
                context.Entry.Entity.ProtectedProperties = new List<string>();

                var result = policy.Check(policyContext, _kernel);
                if (result.IsSuccess == false)
                {
                    context.Entry.Entity.ProtectionResult = Models.ProtectionResults.Deny;
                    context.Entry.Entity.ProtectedProperties = result.RestrictedProperties;
                    // If one of policies fail, we don't want to apply other ones
                    break;
                }
            }
        }
    }
}
