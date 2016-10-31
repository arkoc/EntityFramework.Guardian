using EntityFramework.Guardian.Policies;
using System;

namespace EntityFramework.Guardian.Protection
{
    public class DefaultRetrieveProtector : IRetrieveProtector
    {
        private readonly GuardianKernel _kernel;

        public DefaultRetrieveProtector(GuardianKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }

            _kernel = kernel;

        }
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

                var allow = policy.Check(policyContext, _kernel);
                if (allow == false)
                {
                    // If one of policies fail, we don't want to apply other ones
                    break;
                }
            }
        }
    }
}
