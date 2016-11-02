using EntityFramework.Guardian.Configuration;
using EntityFramework.Guardian.Policies;
using EntityFramework.Guardian.Guards;
using System.Collections.Generic;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// Kernel of the Guardian
    /// </summary>
    public class GuardianKernel
    {
        /// <summary>
        /// Gets the entity key provider.
        /// </summary>
        /// <value>
        /// The entity key provider.
        /// </value>
        public IEntityKeyProvider EntityKeyProvider { get; set; }

        /// <summary>
        /// Gets the modify protector.
        /// </summary>
        /// <value>
        /// The modify protector.
        /// </value>
        public IModifyGuard ModifyGuard { get; set; }

        /// <summary>
        /// Gets the retrieve protector.
        /// </summary>
        /// <value>
        /// The retrieve protector.
        /// </value>
        public IRetrieveGuard RetrieveGuard { get; set; }

        /// <summary>
        /// Gets the modify protection policies.
        /// </summary>
        /// <value>
        /// The modify protection policies.
        /// </value>
        public List<IModifyPolicy> ModifyProtectionPolicies { get; private set; }

        /// <summary>
        /// Gets the retrieve protection policies.
        /// </summary>
        /// <value>
        /// The retrieve protection policies.
        /// </value>
        public List<IRetrievePolicy> RetrieveProtectionPolicies { get; private set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public GuardianPermissions Permissions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuardianKernel"/> class.
        /// </summary>
        public GuardianKernel()
        {
            EntityKeyProvider = new DefaultIdKeyProvider();

            ModifyGuard = new DefaultModifyGuard(this);
            RetrieveGuard = new DefaultRetrieveGuard(this);

            ModifyProtectionPolicies = new List<IModifyPolicy>();
            RetrieveProtectionPolicies = new List<IRetrievePolicy>();

            ModifyProtectionPolicies.Add(new DefaultModifyPolicy());
            RetrieveProtectionPolicies.Add(new DefaultRetrievePolicy());

            Permissions = new GuardianPermissions();
        }
    }
}
