using EntityFramework.Guardian.Models;
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
        /// Gets the modified properties.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>Modified properties of entity</returns>
        public static List<string> GetModifiedProperties(this ObjectContext context, object entity)
        {
            var myObjectState = context.ObjectStateManager.GetObjectStateEntry(entity);
            var modifiedProperties = myObjectState.GetModifiedProperties().ToList();

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

            ObjectStateEntry objectStateEntry;
            if (context.ObjectStateManager.TryGetObjectStateEntry(entity, out objectStateEntry))
            {
                if(entry == null)
                {
                    return success;
                }

                if ((entry is IProtectableObject) && objectStateEntry.State.HasFlag(EntityState.Unchanged))
                {
                    success = true;
                    entry = new ObjectAccessEntry(objectStateEntry);
                }
            }

            return success;
        }
    }
}
