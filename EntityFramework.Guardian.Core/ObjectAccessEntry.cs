using EntityFramework.Guardian.Extensions;
using EntityFramework.Guardian.Models;
using System;
using System.Data.Entity.Core.Objects;

namespace EntityFramework.Guardian
{
    public class ObjectAccessEntry
    {
        public IProtectableObject Entity { get; }
        public AccessTypes AccessType { get; }

        public ObjectAccessEntry(ObjectStateEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            if ((entry.Entity is IProtectableObject) == false)
            {
                throw new InvalidOperationException("Entity for protection doesn't implement IProtectableObject interface.");
            }

            Entity = entry.Entity as IProtectableObject;
            AccessType = entry.State.GetAccessType();
        }
    }
}
