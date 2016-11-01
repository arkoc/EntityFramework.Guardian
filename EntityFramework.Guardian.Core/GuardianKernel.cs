using EntityFramework.Guardian.Configuration;
using EntityFramework.Guardian.Policies;
using EntityFramework.Guardian.Protection;
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
        public IEntityKeyProvider EntityKeyProvider { get; private set; }

        /// <summary>
        /// Gets the modify protector.
        /// </summary>
        /// <value>
        /// The modify protector.
        /// </value>
        public IModifyProtector ModifyProtector { get; private set; }

        /// <summary>
        /// Gets the retrieve protector.
        /// </summary>
        /// <value>
        /// The retrieve protector.
        /// </value>
        public IRetrieveProtector RetrieveProtector { get; private set; }

        /// <summary>
        /// Gets the modify protection policies.
        /// </summary>
        /// <value>
        /// The modify protection policies.
        /// </value>
        public List<IModifyProtectionPolicy> ModifyProtectionPolicies { get; private set; }

        /// <summary>
        /// Gets the retrieve protection policies.
        /// </summary>
        /// <value>
        /// The retrieve protection policies.
        /// </value>
        public List<IRetrieveProtectionPolicy> RetrieveProtectionPolicies { get; private set; }

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

            ModifyProtector = new DefaultModifyProtector(this);
            RetrieveProtector = new DefaultRetrieveProtector(this);

            ModifyProtectionPolicies = new List<IModifyProtectionPolicy>();
            RetrieveProtectionPolicies = new List<IRetrieveProtectionPolicy>();

            ModifyProtectionPolicies.Add(new DefaultModifyPolicy());
            RetrieveProtectionPolicies.Add(new DefaultRetrievePolicy());

            Permissions = new GuardianPermissions();
        }
    }
}
