using EntityFramework.Guardian.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core.Policies
{
    public class DefaultRetrievePolicy : IRetrieveProtectionPolicy
    {
        public bool Check(RetrievePolicyContext context, GuardianKernel kernel)
        {
            var isSuccess = true;
            context.Entity.ProtectionResult = ProtectionResults.Allow;
            var permissions = kernel.Permissions;

            var generalPermissions = permissions.GetGeneralPermission(context.EntityTypeName, context.AccessType);

            var rowLevelPermissions = permissions.GetRowLevelPermission(
                context.EntityTypeName,
                context.EntityRowKey,
                context.AccessType);

            var columnLevelRestrictions = permissions.GetColumnLevelRestrictions(context.EntityTypeName, context.AccessType);

            if (generalPermissions.Any() == false && rowLevelPermissions.Any() == false)
            {
                context.Entity.ProtectionResult = ProtectionResults.Deny;
                isSuccess = false;
                return isSuccess;
            }

            foreach (var columnRestriction in columnLevelRestrictions)
            {
                context.Entity.ProtectedProperties.Add(columnRestriction.PropertyName);
            }

            return isSuccess;
        }
    }
}
