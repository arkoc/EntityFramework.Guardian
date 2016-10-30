using EntityFramework.Guardian.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core.Policies
{
    public class RetrievePolicyContext
    {
        public string EntityTypeName { get; set; }
        public string EntityRowKey { get; set; }
        public AccessTypes AccessType { get; set; }
        public IProtectableObject Entity { get; set; }
    }
}
