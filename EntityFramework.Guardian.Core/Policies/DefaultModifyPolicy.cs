using System.Linq;

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Default implementation of <see cref="IModifyPolicy"/> that checks for built-in permissions
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Policies.IModifyPolicy" />
    public class DefaultModifyPolicy : IModifyPolicy
    {
        /// <summary>
        /// Checks the policy by specified context and kernel.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="kernel">The kernel.</param>
        /// <returns><see cref="ModifyPolicyResult"/> of policy checking.</returns>
        public ModifyPolicyResult Check(ModifyPolicyContext context, GuardianKernel kernel)
        {
            var result = new ModifyPolicyResult();

            var principal = kernel.Principal;

            var generalPermissions = principal.GetGeneralPermissions(context.EntityTypeName, context.AccessType);

            var rowLevelPermissions = principal.GetRowLevelPermissions(
                context.EntityTypeName,
                context.AccessType,
                context.EntityRowKey);

            var columnLevelRestrictions = principal.GetColumnLevelRestrictions(context.EntityTypeName, context.AccessType);

            if (generalPermissions.Any() == false && rowLevelPermissions.Any() == false)
            {
                result.IsSuccess = false;
                return result;
            }

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
