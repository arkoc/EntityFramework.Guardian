using EntityFramework.Guardian.Exceptions;
using EntityFramework.Guardian.Policies;
using System;

namespace EntityFramework.Guardian.Protection
{
    public class DefaultModifyProtector : IModifyProtector
    {
        private readonly GuardianKernel _kernel;

        public DefaultModifyProtector(GuardianKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }

            _kernel = kernel;
        }

        public void Protect(ModifyProtectionContext context)
        {
            foreach (var policy in _kernel.ModifyProtectionPolicies)
            {
                var policyContext = new ModifyPolicyContext()
                {
                    AccessType = context.Entry.AccessType,
                    Entity = context.Entry.Entity,
                    EntityRowKey = _kernel.EntityKeyProvider.GetKey(context.Entry.Entity),
                    EntityTypeName = context.Entry.Entity.GetType().Name,
                    ModifiedProperties = context.ModifiedProperties
                };

                var allow = policy.Apply(policyContext, _kernel);

                if (allow == false)
                {
                    // If one of policies fail, we don't want to apply another ones
                    throw new AccessDeniedException();
                }
            }
        }
    }
}
