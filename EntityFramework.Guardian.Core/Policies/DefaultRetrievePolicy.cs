using EntityFramework.Guardian.Models;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework.Guardian.Policies
{
    public class DefaultRetrievePolicy : IRetrieveProtectionPolicy
    {
        public RetrievePolicyResult Check(RetrievePolicyContext context, GuardianKernel kernel)
        {
            var resut = new RetrievePolicyResult();

            var permissions = kernel.Permissions;

            var generalPermissions = permissions.GetGeneralPermissions(context.EntityTypeName, AccessTypes.Get);

            var rowLevelPermissions = permissions.GetRowLevelPermissions(
                context.EntityTypeName,
                context.EntityRowKey,
                AccessTypes.Get);

            var columnLevelRestrictions = permissions.GetColumnLevelRestrictions(context.EntityTypeName, AccessTypes.Get);

            if (generalPermissions.Any() == false && rowLevelPermissions.Any() == false)
            {
                resut.IsSuccess = false;
                return resut;
            }

            foreach (var columnRestriction in columnLevelRestrictions)
            {
                resut.RestrictedProperties.Add(columnRestriction.PropertyName);
            }

            return resut;
        }
    }
}
