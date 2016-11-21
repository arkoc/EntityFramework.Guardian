// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace EntityFramework.Guardian.Services
{
    /// <summary>
    /// Default key provider that gathers key from Id property of entity
    /// </summary>
    /// <seealso cref="IEntityKeyProvider" />
    public class DefaultIdKeyProvider : IEntityKeyProvider
    {
        /// <summary>
        /// Gets the Id key from entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public string GetKey(object entity)
        {
            var idProperty = entity.GetType().GetProperty("Id");

            if (idProperty == null)
            {
                throw new InvalidOperationException($"DefaultIdKeyProvider can't get Id column {entity.GetType().Name}");
            }

            var value = idProperty.GetValue(entity).ToString();

            return value;
        }
    }
}
