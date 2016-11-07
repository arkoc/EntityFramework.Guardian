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
        public List<IModifyPolicy> ModifyPolicies { get; private set; }

        /// <summary>
        /// Gets the retrieve protection policies.
        /// </summary>
        /// <value>
        /// The retrieve protection policies.
        /// </value>
        public List<IRetrievePolicy> RetrievePolicies { get; private set; }

        /// <summary>
        /// Gets or sets the principal.
        /// </summary>
        /// <value>
        /// The principal.
        /// </value>
        public IDbPrincipal Principal { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuardianKernel"/> class.
        /// </summary>
        public GuardianKernel()
        {
            EntityKeyProvider = new DefaultIdKeyProvider();

            ModifyGuard = new DefaultModifyGuard(this);
            RetrieveGuard = new DefaultRetrieveGuard(this);

            ModifyPolicies = new List<IModifyPolicy>();
            RetrievePolicies = new List<IRetrievePolicy>();

            ModifyPolicies.Add(new PermissionsExistsModifyPolicy());
            ModifyPolicies.Add(new ColumnsRestrictionsModifyPolicy());

            RetrievePolicies.Add(new PermissionExistsRetrievePolicy());
            RetrievePolicies.Add(new ColumnsRestrictionsRetrievePolicy());

            Principal = new DbPrincipal();
        }
    }
}
