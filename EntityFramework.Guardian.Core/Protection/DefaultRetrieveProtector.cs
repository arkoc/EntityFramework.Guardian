using EntityFramework.Guardian.Core.Models;
using EntityFramework.Guardian.Core.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core.Protection
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
                    AccessType = context.Entry.AccessType,
                    Entity = context.Entry.Entity,
                    EntityRowKey = _kernel.EntityKeyProvider.GetKey(context.Entry.Entity),
                    EntityTypeName = context.Entry.Entity.GetType().Name,
                };

                var allow = policy.Check(policyContext, _kernel);
                if(allow == false)
                {
                    // If one of policies fail, we don't want to apply other ones
                    break;
                }
            }
        }
    }
}
