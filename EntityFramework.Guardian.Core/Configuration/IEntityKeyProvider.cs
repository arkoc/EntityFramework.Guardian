using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core.Configuration
{
    public interface IEntityKeyProvider
    {
        string GetKey(object entity);
    }
}
