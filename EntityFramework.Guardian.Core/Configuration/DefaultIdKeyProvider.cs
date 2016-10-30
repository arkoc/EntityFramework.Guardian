using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core.Configuration
{
    class DefaultIdKeyProvider : IEntityKeyProvider
    {
        public string GetKey(object entity)
        {
            var value = GetType().GetProperty("Id").GetValue(entity).ToString();
            return value;
        }
    }
}
