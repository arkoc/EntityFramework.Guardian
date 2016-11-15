// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework.Guardian.Extensions
{
    internal static class PermissionsExtensions
    {
        public static List<IColumnRestriction> SelectColumnRestrictions(this IEnumerable<IPermission> permissions)
        {
            return SelectColumnRestrictions(permissions, typeof(IPermission<>));
        }

        public static List<IColumnRestriction> SelectColumnRestrictions(this IEnumerable<IRowPermission> permissions)
        {
            return SelectColumnRestrictions(permissions, typeof(IRowPermission<>));
        }

        private static List<IColumnRestriction> SelectColumnRestrictions(IEnumerable<object> permissions, Type permissionType)
        {
            var columnRestrictions = new List<IColumnRestriction>();

            var permissionsWithColumnRestrictions = permissions.Where(x => x.GetType().IsAssignableToGenericType(permissionType));

            foreach (var permission in permissionsWithColumnRestrictions)
            {
                var columnRestrictionProperty = permission.GetType().GetProperty("ColumnRestrictions");

                var columnRestrictionsInPermission =
                    columnRestrictionProperty.GetValue(permission) as IEnumerable<IColumnRestriction>;

                if (columnRestrictionsInPermission != null
                    && columnRestrictionsInPermission.Any())
                {
                    columnRestrictions.AddRange(columnRestrictionsInPermission);
                }
            }

            return columnRestrictions;
        }
    }
}
