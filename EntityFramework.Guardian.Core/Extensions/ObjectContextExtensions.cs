// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;

namespace EntityFramework.Guardian.Extensions
{
    internal static class ObjectContextExtensions
    {
        public static IReadOnlyDictionary<string, object> GetModifiedOriginalValues(this ObjectContext context, object entity)
        {
            var originalValues = new Dictionary<string, object>();
            var objectState = context.ObjectStateManager.GetObjectStateEntry(entity);

            if (objectState.State.HasFlag(EntityState.Added))
            {
                return null;
            }

            var modifiedProperties = objectState.GetModifiedProperties();
            foreach (var propName in modifiedProperties)
            {
                originalValues.Add(propName, objectState.OriginalValues[propName]);
            }

            return originalValues;

        }

        public static IReadOnlyDictionary<string, object> GetModifiedLocalValues(this ObjectContext context, object entity)
        {
            var localValues = new Dictionary<string, object>();
            var objectState = context.ObjectStateManager.GetObjectStateEntry(entity);

            IEnumerable<string> modifiedProperties;

            if (objectState.State.HasFlag(EntityState.Added))
            {
                modifiedProperties = GetInitializedProperties(entity);
            }
            else
            {
                modifiedProperties = objectState.GetModifiedProperties();
            }
           
            foreach (var propName in modifiedProperties)
            {
                localValues.Add(propName, objectState.CurrentValues[propName]);
            }

            return localValues;
        }

        public static IEnumerable<IObjectAccessEntry> GetModifiedEntries(this ObjectContext context)
        {
            var reasonableStates = EntityState.Modified | EntityState.Deleted | EntityState.Added;

            var entries = context.ObjectStateManager
             .GetObjectStateEntries(reasonableStates)
             .Where(entry => entry.Entity is IProtectableObject)
             .Select(entry => new ObjectAccessEntry(entry));

            return entries;
        }


        public static bool TryGetMaterializedEntry(this ObjectContext context, object entity, out IObjectAccessEntry entry)
        {
            bool success = false;
            entry = null;

            if ((entity is IProtectableObject) == false)
            {
                return false;
            }

            ObjectStateEntry objectStateEntry;
            if (context.ObjectStateManager.TryGetObjectStateEntry(entity, out objectStateEntry))
            {
                if (objectStateEntry == null)
                {
                    return false;
                }

                if (objectStateEntry.State.HasFlag(EntityState.Unchanged))
                {
                    success = true;
                    entry = new ObjectAccessEntry(objectStateEntry);
                }
            }

            return success;
        }

        private static IEnumerable<string> GetInitializedProperties(object entity)
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
