// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Default implementation of <see cref="IAlteringPolicy"/> that checks for built-in permissions
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Policies.IAlteringPolicy" />
    public class PermissionsExistsAlteringPolicy : IAlteringPolicy
    {
        /// <summary>
        /// Checks the policy by specified context and kernel.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><see cref="AlteringPolicyResult"/> of policy checking.</returns>
        public AlteringPolicyResult Check(AlteringPolicyContext context)
        {
            var result = new AlteringPolicyResult();

            if (context.Permissions.GeneralPermissions.Any() == false
                && context.Permissions.RowLevelPermissions.Any() == false)
            {
                result.IsSuccess = false;
                return result;
            }

            return result;
        }
    }
}
