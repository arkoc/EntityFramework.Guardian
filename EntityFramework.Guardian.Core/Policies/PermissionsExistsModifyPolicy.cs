using System.Linq;

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Default implementation of <see cref="IModifyPolicy"/> that checks for built-in permissions
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Policies.IModifyPolicy" />
    public class PermissionsExistsModifyPolicy : IModifyPolicy
    {
        /// <summary>
        /// Checks the policy by specified context and kernel.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><see cref="ModifyPolicyResult"/> of policy checking.</returns>
        public ModifyPolicyResult Check(ModifyPolicyContext context)
        {
            var result = new ModifyPolicyResult();

            if (context.GeneralPermissions.Any() == false && context.RowLevelPermissions.Any() == false)
            {
                result.IsSuccess = false;
                return result;
            }

            return result;
        }
    }
}
