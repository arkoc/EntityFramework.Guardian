using EntityFramework.Guardian.Core.Models;

namespace EntityFramework.Guardian.Core.Policies
{
    public class RetrievePolicyContext
    {
        public string EntityTypeName { get; set; }
        public string EntityRowKey { get; set; }
        public IProtectableObject Entity { get; set; }
    }
}
