// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Policies;
using EntityFramework.Guardian.Guards;
using System.Data.Entity;
using EntityFramework.Guardian.Exceptions;
using EntityFramework.Guardian.Services;
using System.Collections.Generic;

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
        /// Gets or sets the permission service.
        /// </summary>
        /// <value>
        /// The permission service.
        /// </value>
        public IPermissionService PermissionService { get; set; }

        /// <summary>
        /// Gets or sets the entity key provider.
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
        public IAlteringGuard ModifyGuard { get; set; }

        /// <summary>
        /// Gets the retrieve protector.
        /// </summary>
        /// <value>
        /// The retrieve protector.
        /// </value>
        public IRetrievalGuard RetrieveGuard { get; set; }

        /// <summary>
        /// Gets the modify protection policies.
        /// </summary>
        /// <value>
        /// The modify protection policies.
        /// </value>
        public List<IAlteringPolicy> ModifyPolicies { get; private set; } = new List<IAlteringPolicy>();

        /// <summary>
        /// Gets the retrieve protection policies.
        /// </summary>
        /// <value>
        /// The retrieve protection policies.
        /// </value>
        public List<IRetrievalPolicy> RetrievePolicies { get; private set; } = new List<IRetrievalPolicy>();

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
            EntityKeyProvider = new DefaultIdKeyProvider();
            PermissionService = new InMemoryPermissionService();

            ModifyGuard = new DefaultAlteringGuard(this);
            RetrieveGuard = new DefaultRetrievalGuard(this);

            ModifyPolicies.Add(new PermissionsExistsAlteringPolicy());
            ModifyPolicies.Add(new ColumnsRestrictionsAlteringPolicy());

            RetrievePolicies.Add(new PermissionExistsRetrievalPolicy());
            RetrievePolicies.Add(new ColumnsRestrictionsRetrievalPolicy());
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

            if (PermissionService == null)
            {
                throw new BadConfigurationException("PermissionService can't be null.");
            }

            if (EntityKeyProvider == null)
            {
                throw new BadConfigurationException("Services.EntityKeyProvider can't be null.");
            }

            if (ModifyGuard == null)
            {
                throw new BadConfigurationException("Guards.ModifyGuard can't be null.");
            }

            if (RetrieveGuard == null)
            {
                throw new BadConfigurationException("Guards.RetrieveGuard can't be null.");
            }
        }
    }
}
