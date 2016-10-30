using EntityFramework.Guardian.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace EntityFramework.Guardian.Core.Extensions
{
    internal static class ObjectContextExtensions
    {
        public static IEnumerable<string> GetModifiedProperties(this ObjectContext context, object entity)
        {
            var myObjectState = context.ObjectStateManager.GetObjectStateEntry(entity);
            var modifiedProperties = myObjectState.GetModifiedProperties();

            return modifiedProperties;
        }

        public static IEnumerable<ObjectAccessEntry> GetModifiedEntries(this ObjectContext context)
        {
            var reasonableStates = EntityState.Modified | EntityState.Deleted | EntityState.Added;

            var entries = context.ObjectStateManager
             .GetObjectStateEntries(reasonableStates)
             .Where(entry => entry.Entity is IProtectableObject)
             .Select(entry => new ObjectAccessEntry(entry));

            return entries;
        }

        public static bool TryGetMaterializedEntry(this ObjectContext context, object entity, out ObjectAccessEntry entry)
        {
            bool success = false;
            entry = null;

            ObjectStateEntry objectStateEntry;
            if (context.ObjectStateManager.TryGetObjectStateEntry(entity, out objectStateEntry))
            {
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
