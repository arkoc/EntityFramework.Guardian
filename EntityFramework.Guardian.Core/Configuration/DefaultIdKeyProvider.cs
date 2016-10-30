using System;

namespace EntityFramework.Guardian.Configuration
{
    public class DefaultIdKeyProvider : IEntityKeyProvider
    {
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
