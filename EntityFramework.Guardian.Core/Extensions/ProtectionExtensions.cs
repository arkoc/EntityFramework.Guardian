using EntityFramework.Guardian.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core.Extensions
{
    public static class ProtectionExtensions
    {
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

        private static void ProtectProperties<TEntity>(TEntity entity) where TEntity : IProtectableObject
        {
            if (entity.ProtectedProperties == null) { return; }

            foreach (var protectedProperty in entity.ProtectedProperties)
            {
                SetDefaultValue(entity, protectedProperty);
            }
        }

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
