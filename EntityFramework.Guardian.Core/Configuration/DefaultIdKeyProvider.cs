using System;

namespace EntityFramework.Guardian.Configuration
{
    /// <summary>
    /// Default key provider that gathers key from Id property of entity
    /// </summary>
    /// <seealso cref="EntityFramework.Guardian.Configuration.IEntityKeyProvider" />
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
                throw new InvalidOperationException($"DefaultIdKeyProvider cant get Id column {entity.GetType().Name}");
            }

            var value = idProperty.GetValue(entity).ToString();

            return value;
        }
    }
}
