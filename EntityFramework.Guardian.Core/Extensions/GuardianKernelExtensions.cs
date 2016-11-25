// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Policies;

namespace EntityFramework.Guardian.Extensions
{
    /// <summary>
    /// <see cref="GuardianKernel"/> extensions
    /// </summary>
    public static class GuardianKernelExtensions
    {
        /// <summary>
        /// Adds policy to GuardianKernel.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        public static GuardianKernel UsePolicy(this GuardianKernel kernel, IQueryPolicy policy)
        {
            kernel.QueryPolicies.Add(policy);
            return kernel;
        }

        /// <summary>
        /// Adds policy to GuardianKernel.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        public static GuardianKernel UsePolicy(this GuardianKernel kernel, ISubmitPolicy policy)
        {
            kernel.SubmitPolicies.Add(policy);
            return kernel;
        }
    }
}
