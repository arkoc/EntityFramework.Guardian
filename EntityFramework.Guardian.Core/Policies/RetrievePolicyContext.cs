using EntityFramework.Guardian.Models;
using System.Collections.Generic;

namespace EntityFramework.Guardian.Policies
{
    public class RetrievePolicyContext
    {
        public string EntityTypeName { get; set; }
        public string EntityRowKey { get; set; }
        public IProtectableObject Entity { get; set; }
    }
}
