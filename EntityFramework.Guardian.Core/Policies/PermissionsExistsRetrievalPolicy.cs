// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Policy for checking permissions retrieved by permissionservice
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Policies.IRetrievalPolicy" />
    public class PermissionExistsRetrievalPolicy : IRetrievalPolicy
    {
        /// <summary>
        /// Checks the policy by specified context and kernel.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><see cref="RetrievalPolicyResult"/> of policy checking.</returns>
        public RetrievalPolicyResult Check(RetrievalPolicyContext context)
        {
            var resut = new RetrievalPolicyResult();

            if (context.Permissions.GeneralPermissions.Any() == false
                && context.Permissions.RowLevelPermissions.Any() == false)
            {
                resut.IsSuccess = false;
                return resut;
            }

            return resut;
        }
    }
}
