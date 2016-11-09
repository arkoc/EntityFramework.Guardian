using EntityFramework.Guardian.Configuration;
using EntityFramework.Guardian.Policies;
using EntityFramework.Guardian.Guards;
using System.Data.Entity;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// Kernel of the Guardian
    /// </summary>
    public class GuardianKernel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [enable guards].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable guards]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableGuards { get; set; }

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        public KernelServices Services { get; } = new KernelServices();

        /// <summary>
        /// Gets or sets the guards.
        /// </summary>
        /// <value>
        /// The guards.
        /// </value>
        public KernelGuards Guards { get; } = new KernelGuards();

        /// <summary>
        /// Gets or sets the policies.
        /// </summary>
        /// <value>
        /// The policies.
        /// </value>
        public KernelPolicies Policies { get; } = new KernelPolicies();

        /// <summary>
        /// Gets or sets the principal.
        /// </summary>
        /// <value>
        /// The principal.
        /// </value>
        public IDbPrincipal Principal { get; set; }

        /// <summary>
        /// Gets or sets the database context associated with this kernel.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        public DbContext DbContext { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuardianKernel"/> class.
        /// </summary>
        public GuardianKernel()
        {
            Services.EntityKeyProvider = new DefaultIdKeyProvider();

            Guards.ModifyGuard = new DefaultModifyGuard(this);
            Guards.RetrieveGuard = new DefaultRetrieveGuard(this);

            Policies.ModifyPolicies.Add(new PermissionsExistsModifyPolicy());
            Policies.ModifyPolicies.Add(new ColumnsRestrictionsModifyPolicy());

            Policies.RetrievePolicies.Add(new PermissionExistsRetrievePolicy());
            Policies.RetrievePolicies.Add(new ColumnsRestrictionsRetrievePolicy());

            Principal = new DbPrincipal();
        }
    }
}
