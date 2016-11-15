// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace EntityFramework.Guardian.Extensions
{
    /// <summary>
    /// <see cref="ObjectContext"/> extensions
    /// </summary>
    internal static class ObjectContextExtensions
    {
        /// <summary>
        /// Gets the affected properties.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>Affected properties of entity</returns>
        public static List<string> GetAffectedProperties(this ObjectContext context, object entity)
        {
            var objectState = context.ObjectStateManager.GetObjectStateEntry(entity);
            var modifiedProperties = objectState.GetModifiedProperties().ToList();

            if (objectState.State.HasFlag(EntityState.Added))
            {
                modifiedProperties.AddRange(entity.GetInitializedProperties());
            }

            modifiedProperties = modifiedProperties.Distinct().ToList();

            return modifiedProperties;
        }

        /// <summary>
        /// Gets the modified entries.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Modified entries in the context.</returns>
        public static IEnumerable<IObjectAccessEntry> GetModifiedEntries(this ObjectContext context)
        {
            var reasonableStates = EntityState.Modified | EntityState.Deleted | EntityState.Added;

            var entries = context.ObjectStateManager
             .GetObjectStateEntries(reasonableStates)
             .Where(entry => entry.Entity is IProtectableObject)
             .Select(entry => new ObjectAccessEntry(entry));

            return entries;
        }

        /// <summary>
        /// Tries the get materialized entry.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="entry">The entry.</param>
        /// <returns>Entry that is currently being materialized.</returns>
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
    }
}
