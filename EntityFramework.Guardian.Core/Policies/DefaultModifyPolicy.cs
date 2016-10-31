using System.Linq;

namespace EntityFramework.Guardian.Policies
{
    public class DefaultModifyPolicy : IModifyProtectionPolicy
    {
        public bool Apply(ModifyPolicyContext context, GuardianKernel kernel)
        {
            var isSuccess = true;
            var permissions = kernel.Permissions;

            var generalPermissions = permissions.GetGeneralPermissions(context.EntityTypeName, context.AccessType);

            var rowLevelPermissions = permissions.GetRowLevelPermissions(
                context.EntityTypeName,
                context.EntityRowKey,
                context.AccessType);

            var columnLevelRestrictions = permissions.GetColumnLevelRestrictions(context.EntityTypeName, context.AccessType);

            if (generalPermissions.Any() == false && rowLevelPermissions.Any() == false)
            {
                isSuccess = false;
                return isSuccess;
            }

            foreach (var propName in context.ModifiedProperties)
            {
                var restrictionExist = columnLevelRestrictions.Any(x => x.PropertyName == propName);

                if (restrictionExist)
                {
                    isSuccess = false;
                    // If Restriction exist it means that our policy faild
                    break;
                }
            }

            return isSuccess;
        }
    }
}
