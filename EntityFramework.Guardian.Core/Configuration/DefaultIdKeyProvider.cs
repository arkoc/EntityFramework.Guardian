using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core.Configuration
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
