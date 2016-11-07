namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// 
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

            var columnLevelRestrictions = context.ColumnRestrictions;

            foreach (var columnRestriction in columnLevelRestrictions)
            {
                result.RestrictedProperties.Add(columnRestriction.PropertyName);
            }

            return result;
        }
    }
}
