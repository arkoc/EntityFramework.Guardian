// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Reflection;

namespace EntityFramework.Guardian.Extensions
{
    /// <summary>
    /// <see cref="object"/> extensions
    /// </summary>
    internal static class ObjectExtesions
    {
        /// <summary>
        /// Gets the initialized properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static List<string> GetInitializedProperties(this object entity)
        {
            var initializedProperties = new List<string>();
            var props = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var value = prop?.GetValue(entity);

                if (value == null) { continue; }

                if (!object.Equals(value, (value.GetType().GetDefaultValue())))
                {
                    initializedProperties.Add(prop.Name);
                }
            }

            return initializedProperties;
        }
    }
}
