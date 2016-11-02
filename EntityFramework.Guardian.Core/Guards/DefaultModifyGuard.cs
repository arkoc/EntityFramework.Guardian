using EntityFramework.Guardian.Exceptions;
using EntityFramework.Guardian.Policies;
using System;

namespace EntityFramework.Guardian.Guards
{
    /// <summary>
    /// Default implementation of IModifyGuard.
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Guards.IModifyGuard" />
    public class DefaultModifyGuard : IModifyGuard
    {
        private readonly GuardianKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultModifyGuard"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <exception cref="System.ArgumentNullException">kernel</exception>
        public DefaultModifyGuard(GuardianKernel kernel)
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

                var result = policy.Check(policyContext, _kernel);

                if (result.IsSuccess == false)
                {
                    // If one of policies fail, we don't want to apply another ones
                    throw new AccessDeniedException();
                }
            }
        }
    }
}
