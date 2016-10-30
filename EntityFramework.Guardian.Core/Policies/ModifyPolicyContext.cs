using EntityFramework.Guardian.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core.Policies
{
    public class ModifyPolicyContext
    {
        public string EntityTypeName { get; set; }
        public string EntityRowKey { get; set; }
        public AccessTypes AccessType { get; set; }
        public IProtectableObject Entity { get; set; }
        public IEnumerable<string> ModifiedProperties { get; set; }
    }
}
