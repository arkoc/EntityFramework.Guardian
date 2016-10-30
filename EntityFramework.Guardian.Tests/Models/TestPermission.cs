using EntityFramework.Guardian.Core.Models;

namespace EntityFramework.Guardian.Tests.Models
{
    class TestPermission : IPermission
    {
        public AccessTypes AccessType { get; set; }
        public string EntityTypeName { get; set; }
    }
}
