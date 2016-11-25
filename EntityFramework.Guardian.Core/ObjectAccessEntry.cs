// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Extensions;
using System;
using System.Data.Entity.Core.Objects;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// ObjectAccess Adapter
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
        /// Gets the type of the action.
        /// </summary>
        /// <value>
        /// The type of the access.
        /// </value>
        public ActionTypes ActionType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectAccessEntry"/> class.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <exception cref="System.ArgumentNullException">entry</exception>
        /// <exception cref="System.InvalidOperationException">Entity for protection doesn't implement IProtectableObject interface.</exception>
        public ObjectAccessEntry(ObjectStateEntry entry)
        {
            Ensure.NotNull(entry, nameof(entry));

            if ((entry.Entity is IProtectableObject) == false)
            {
                throw new InvalidOperationException("Entity for protection doesn't implement IProtectableObject interface.");
            }

            Entity = entry.Entity as IProtectableObject;
            ActionType = entry.State.GetActionType();
        }
    }
}
