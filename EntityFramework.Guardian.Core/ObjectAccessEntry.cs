using EntityFramework.Guardian.Extensions;
using EntityFramework.Guardian.Models;
using System;
using System.Data.Entity.Core.Objects;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.IObjectAccessEntry" />
    internal class ObjectAccessEntry : IObjectAccessEntry
    {
        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public IProtectableObject Entity { get; }

        /// <summary>
        /// Gets the type of the access.
        /// </summary>
        /// <value>
        /// The type of the access.
        /// </value>
        public AccessTypes AccessType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectAccessEntry"/> class.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <exception cref="System.ArgumentNullException">entry</exception>
        /// <exception cref="System.InvalidOperationException">Entity for protection doesn't implement IProtectableObject interface.</exception>
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
