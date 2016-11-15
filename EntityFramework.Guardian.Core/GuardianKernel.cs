using EntityFramework.Guardian.Policies;
using EntityFramework.Guardian.Guards;
using System.Data.Entity;
using EntityFramework.Guardian.Exceptions;
using EntityFramework.Guardian.Services;

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
        public bool EnableGuards { get; set; } = true;

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
            Services.PermissionService = new DefaultPermissionService();

            Guards.ModifyGuard = new DefaultModifyGuard(this);
            Guards.RetrieveGuard = new DefaultRetrieveGuard(this);

            Policies.ModifyPolicies.Add(new PermissionsExistsModifyPolicy());
            Policies.ModifyPolicies.Add(new ColumnsRestrictionsModifyPolicy());

            Policies.RetrievePolicies.Add(new PermissionExistsRetrievePolicy());
            Policies.RetrievePolicies.Add(new ColumnsRestrictionsRetrievePolicy());
        }

        /// <summary>
        /// Tries to validate guardian kernel
        /// </summary>
        /// <exception cref="EntityFramework.Guardian.Exceptions.BadConfigurationException">
        /// </exception>
        public void TryValidate()
        {
            if (DbContext != null)
            {
                throw new BadConfigurationException("Specified GuardianKernel object already associcated with another DbContext.");
            }

            if (Services.PermissionService == null)
            {
                throw new BadConfigurationException("Services.PermissionService can't be null.");
            }

            if (Services.EntityKeyProvider == null)
            {
                throw new BadConfigurationException("Services.EntityKeyProvider can't be null.");
            }

            if (Guards.ModifyGuard == null)
            {
                throw new BadConfigurationException("Guards.ModifyGuard can't be null.");
            }

            if (Guards.RetrieveGuard == null)
            {
                throw new BadConfigurationException("Guards.RetrieveGuard can't be null.");
            }
        }
    }
}
