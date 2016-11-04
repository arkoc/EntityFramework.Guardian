using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Extensions;
using System.Linq;

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Default implementation of <see cref="IRetrievePolicy"/> that checks for built-in permissions
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Policies.IRetrievePolicy" />
    public class DefaultRetrievePolicy : IRetrievePolicy
    {
        /// <summary>
        /// Checks the policy by specified context and kernel.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="kernel">The kernel.</param>
        /// <returns><see cref="RetrievePolicyResult"/> of policy checking.</returns>
        public RetrievePolicyResult Check(RetrievePolicyContext context, GuardianKernel kernel)
        {
            var resut = new RetrievePolicyResult();

            var principal = kernel.Principal;

            var generalPermissions = principal.GetGeneralPermissions(context.EntityTypeName, AccessTypes.Get);

            var rowLevelPermissions = principal.GetRowLevelPermissions(
                context.EntityTypeName,
                AccessTypes.Get,
                context.EntityRowKey);

            if (generalPermissions.Any() == false && rowLevelPermissions.Any() == false)
            {
                resut.IsSuccess = false;
                return resut;
            }

            var columnLevelRestrictionInGeneral = generalPermissions.SelectColumnRestrictions();
            var columnLevelRestrictionInRow = rowLevelPermissions.SelectColumnRestrictions();

            var columnLevelRestrictions = columnLevelRestrictionInGeneral.Concat(columnLevelRestrictionInRow);

            foreach (var columnRestriction in columnLevelRestrictions)
            {
                resut.RestrictedProperties.Add(columnRestriction.PropertyName);
            }

            return resut;
        }
    }
}
