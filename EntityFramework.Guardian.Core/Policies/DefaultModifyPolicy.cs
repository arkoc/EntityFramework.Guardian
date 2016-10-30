using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core.Policies
{
    public class DefaultModifyPolicy : IModifyProtectionPolicy
    {
        public bool Check(ModifyPolicyContext context, GuardianKernel kernel)
        {
            var success = false;
            var permissions = kernel.Permissions;

            var generalPermissions = permissions.GetGeneralPermission(context.EntityTypeName, context.AccessType);

            var rowLevelPermissions = permissions.GetRowLevelPermission(
                context.EntityTypeName,
                context.EntityRowKey,
                context.AccessType);

            var columnLevelRestrictions = permissions.GetColumnLevelRestrictions(context.EntityTypeName, context.AccessType);

            if (generalPermissions.Any() || rowLevelPermissions.Any())
            {
                success = true;
                return success;
            }

            foreach (var propName in context.ModifiedProperties)
            {
                var restrictionExist = columnLevelRestrictions.Any(x => x.PropertyName == propName);

                if (restrictionExist)
                {
                    success = false;
                    // If Restriction exist it means that our policy faild
                    break;
                }
            }

            return success;
        }
    }
}
