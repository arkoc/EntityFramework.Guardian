﻿using EntityFramework.Guardian.Models;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework.Guardian.Policies
{
    public class DefaultRetrievePolicy : IRetrieveProtectionPolicy
    {
        public bool Apply(RetrievePolicyContext context, GuardianKernel kernel)
        {
            var isSuccess = true;

            context.Entity.ProtectedProperties = new List<string>();
            context.Entity.ProtectionResult = ProtectionResults.Allow;

            var permissions = kernel.Permissions;

            var generalPermissions = permissions.GetGeneralPermissions(context.EntityTypeName, AccessTypes.Get);

            var rowLevelPermissions = permissions.GetRowLevelPermissions(
                context.EntityTypeName,
                context.EntityRowKey,
                AccessTypes.Get);

            var columnLevelRestrictions = permissions.GetColumnLevelRestrictions(context.EntityTypeName, AccessTypes.Get);

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
