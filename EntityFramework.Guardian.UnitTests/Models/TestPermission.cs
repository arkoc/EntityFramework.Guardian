using EntityFramework.Guardian.Entities;

namespace EntityFramework.Guardian.UnitTests.Models
{
    class TestPermission : IPermission
    {
        public AccessTypes AccessType { get; set; }
        public string EntityTypeName { get; set; }
    }
}
