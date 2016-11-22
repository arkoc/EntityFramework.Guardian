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
        public bool EnableGuards { get; set; }

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
        /// Gets the altering guard.
        /// </summary>
        /// <value>
        /// The altering guard.
        /// </value>
        public IAlteringGuard AlteringGuard { get; set; }

        /// <summary>
        /// Gets the retrieval guard.
        /// </summary>
        /// <value>
        /// The retrieval guard.
        /// </value>
        public IRetrievalGuard RetrievalGuard { get; set; }

        /// <summary>
        /// Gets the altering protection policies.
        /// </summary>
        /// <value>
        /// The modify protection policies.
        /// </value>
        public List<IAlteringPolicy> AlteringPolicies { get; private set; }

        /// <summary>
        /// Gets the retrieval protection policies.
        /// </summary>
        /// <value>
        /// The retrieve protection policies.
        /// </value>
        public List<IRetrievalPolicy> RetrievalPolicies { get; private set; }

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
            EnableGuards = true;

            EntityKeyProvider = new DefaultIdKeyProvider();
            PermissionService = new InMemoryPermissionService();

            AlteringGuard = new DefaultAlteringGuard(this);
            RetrievalGuard = new DefaultRetrievalGuard(this);

            AlteringPolicies = new List<IAlteringPolicy>();
            AlteringPolicies.Add(new PermissionsExistsAlteringPolicy());
            AlteringPolicies.Add(new ColumnsRestrictionsAlteringPolicy());

            RetrievalPolicies = new List<IRetrievalPolicy>();
            RetrievalPolicies.Add(new PermissionExistsRetrievalPolicy());
            RetrievalPolicies.Add(new ColumnsRestrictionsRetrievalPolicy());
        }

        /// <summary>
        /// Trys to validate guardian kernel
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
                throw new BadConfigurationException($"{nameof(PermissionService)} can't be null.");
            }

            if (EntityKeyProvider == null)
            {
                throw new BadConfigurationException($"{nameof(EntityKeyProvider)} can't be null.");
            }

            if (AlteringGuard == null)
            {
                throw new BadConfigurationException($"{nameof(AlteringGuard)} can't be null.");
            }

            if (RetrievalGuard == null)
            {
                throw new BadConfigurationException($"{nameof(RetrievalGuard)} can't be null.");
            }
        }
    }
}
