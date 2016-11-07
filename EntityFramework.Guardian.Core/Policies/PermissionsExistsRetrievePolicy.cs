using System.Linq;

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// 
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

            if (context.GeneralPermissions.Any() == false 
                && context.RowLevelPermissions.Any() == false)
            {
                resut.IsSuccess = false;
                return resut;
            }

            return resut;
        }
    }
}
