using EntityFramework.Guardian.Models;
using System;
using System.Linq;
using System.Reflection;

namespace EntityFramework.Guardian.Extensions
{
    /// <summary>
    /// <see cref="IQueryable{IProtectableObject}"/> extensions
    /// </summary>
    public static class ProtectionExtensions
    {
        /// <summary>
        /// Protects the specified source.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>Protected source.</returns>
        public static IQueryable<TEntity> Protect<TEntity>(this IQueryable<TEntity> source) where TEntity : class, IProtectableObject
        {
            if (source == null)
            {
                return null;
            }

            var protectedSource = source.ToList().Where(x => x.ProtectionResult == ProtectionResults.Allow).ToList();
            foreach (var entry in protectedSource)
            {
                ProtectProperties(entry);
            }

            return protectedSource.AsQueryable();
        }

        /// <summary>
        /// Protects the specified source.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>Protected source.</returns>
        public static TEntity Protect<TEntity>(this TEntity source) where TEntity : class, IProtectableObject
        {
            if (source == null)
            {
                return null;
            }

            TEntity result = null;
            if (source.ProtectionResult == ProtectionResults.Allow)
            {
                ProtectProperties(source);
                result = source;
            }

            return result;
        }

        /// <summary>
        /// Protects the entiry properties.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        private static void ProtectProperties<TEntity>(TEntity entity) where TEntity : IProtectableObject
        {
            if (entity.ProtectedProperties == null) { return; }

            foreach (var protectedProperty in entity.ProtectedProperties)
            {
                SetDefaultValue(entity, protectedProperty);
            }
        }

        /// <summary>
        /// Sets the default value to property.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="propName">Name of the property.</param>
        private static void SetDefaultValue(object obj, string propName)
        {
            var objType = obj.GetType();

            var prop = objType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(x => string.Compare(x.Name, propName, StringComparison.OrdinalIgnoreCase) == 0);

            prop?.SetValue(obj, null, null);
        }
    }
}
