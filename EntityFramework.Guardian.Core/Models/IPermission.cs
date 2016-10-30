using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core.Models
{
    public interface IPermission
    {
        string EntityTypeName { get; set; }
        AccessTypes AccessType { get; set; }
    }
}
