﻿// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Policy for applying Column Restrictions when modifing entities
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Policies.IModifyPolicy" />
    public class ColumnsRestrictionsModifyPolicy : IModifyPolicy
    {
        /// <summary>
        /// Checks the policy by specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        ///   <see cref="ModifyPolicyResult" /> of policy checking.
        /// </returns>
        public ModifyPolicyResult Check(ModifyPolicyContext context)
        {
            var result = new ModifyPolicyResult();

            var columnLevelRestrictions = context.Permissions.ColumnRestrictions;

            foreach (var propName in context.ModifiedProperties)
            {
                var restrictionExist = columnLevelRestrictions.Any(x => x.PropertyName == propName);

                if (restrictionExist)
                {
                    result.IsSuccess = false;
                    // If Restriction exist it means that our policy faild
                    break;
                }
            }

            return result;
        }
    }
}
