using System.Collections.Generic;
using EntityFramework.Guardian.Models;

namespace EntityFramework.Guardian.UnitTests.Models
{
    internal class Model1 : IProtectableObject
    {
        public int Id { get; set; }
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public List<string> ProtectedProperties { get; set; }
        public ProtectionResults ProtectionResult { get; set; }
    }
}
