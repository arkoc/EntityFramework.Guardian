using EntityFramework.Guardian.Core.Exceptions;
using EntityFramework.Guardian.Core.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core.Protection
{
    public class DefaultModifyProtector : IModifyProtector
    {
        private GuardianKernel _kernel;

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
            foreach(var policy in _kernel.ModifyProtectionPolicies)
            {
                var policyContext = new ModifyPolicyContext()
                {
                    AccessType = context.Entry.AccessType,
                    Entity = context.Entry.Entity,
                    EntityRowKey = _kernel.EntityKeyProvider.GetKey(context.Entry.Entity),
                    EntityTypeName = context.Entry.Entity.GetType().Name,
                    ModifiedProperties = context.ModifiedProperties
                };

                var allow = policy.Check(policyContext, _kernel);

                if(allow == false)
                {
                    // If one of policies fail, we don't want to apply another ones
                    throw new AccessDeniedException();
                }
            }
        }
    }
}
