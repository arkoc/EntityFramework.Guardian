// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Policies;
using EntityFramework.Guardian.Guards;
using System.Collections.Generic;
using System;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// Kernel of the Guardian
    /// </summary>
    public class GuardianKernel
    {
        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable guards].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable guards]; otherwise, <c>disable guards</c>.
        /// </value>
        public bool EnableGuards { get; set; }

        /// <summary>
        /// Gets the query guard.
        /// </summary>
        /// <value>
        /// The retrieval guard.
        /// </value>
        public IQueryGuard QueryGuard { get; set; }

        /// <summary>
        /// Gets the submit guard.
        /// </summary>
        /// <value>
        /// The altering guard.
        /// </value>
        public ISubmitGuard SubmitGuard { get; set; }

        /// <summary>
        /// Gets the querying protection policies.
        /// </summary>
        /// <value>
        /// The retrieve protection policies.
        /// </value>
        public List<IQueryPolicy> QueryPolicies { get; }

        /// <summary>
        /// Gets the submiting protection policies.
        /// </summary>
        /// <value>
        /// The modify protection policies.
        /// </value>
        public List<ISubmitPolicy> SubmitPolicies { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuardianKernel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public GuardianKernel(IServiceProvider serviceProvider = null)
        {
            ServiceProvider = serviceProvider;

            EnableGuards = true;

            SubmitGuard = new DefaultSubmitGuard();
            QueryGuard = new DefaultQueryGuard();

            SubmitPolicies = new List<ISubmitPolicy>();
            QueryPolicies = new List<IQueryPolicy>();
        }

        /// <summary>
        /// Trys to validate guardian kernel
        /// </summary>
        /// <exception cref="Exception">
        /// </exception>
        public void TryValidate()
        {
            Ensure.NotNull(SubmitGuard, nameof(SubmitGuard));
            Ensure.NotNull(QueryGuard, nameof(QueryGuard));
        }
    }
}
