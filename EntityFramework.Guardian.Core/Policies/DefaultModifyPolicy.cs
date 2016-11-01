using System.Linq;

namespace EntityFramework.Guardian.Policies
{
    public class DefaultModifyPolicy : IModifyProtectionPolicy
    {
        public ModifyPolicyResult Check(ModifyPolicyContext context, GuardianKernel kernel)
        {
            var result = new ModifyPolicyResult();

            var permissions = kernel.Permissions;

            var generalPermissions = permissions.GetGeneralPermissions(context.EntityTypeName, context.AccessType);

            var rowLevelPermissions = permissions.GetRowLevelPermissions(
                context.EntityTypeName,
                context.EntityRowKey,
                context.AccessType);

            var columnLevelRestrictions = permissions.GetColumnLevelRestrictions(context.EntityTypeName, context.AccessType);

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
