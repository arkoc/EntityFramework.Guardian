// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Policy for applying Column Restrictions when retrieving entities
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Policies.IRetrievePolicy" />
    public class ColumnsRestrictionsRetrievePolicy : IRetrievePolicy
    {
        /// <summary>
        /// Checks the policy by specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        ///   <see cref="RetrievePolicyResult" /> of policy checking.
        /// </returns>
        public RetrievePolicyResult Check(RetrievePolicyContext context)
        {
            var result = new RetrievePolicyResult();

            var columnLevelRestrictions = context.Permissions.ColumnRestrictions;

            foreach (var columnRestriction in columnLevelRestrictions)
            {
                result.RestrictedProperties.Add(columnRestriction.PropertyName);
            }

            return result;
        }
    }
}
