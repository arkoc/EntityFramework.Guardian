// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Policy for checking permissions retrieved by permissionservice
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Policies.IRetrievePolicy" />
    public class PermissionExistsRetrievePolicy : IRetrievePolicy
    {
        /// <summary>
        /// Checks the policy by specified context and kernel.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><see cref="RetrievePolicyResult"/> of policy checking.</returns>
        public RetrievePolicyResult Check(RetrievePolicyContext context)
        {
            var resut = new RetrievePolicyResult();

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
