using EntityFramework.Guardian.Entities;

namespace EntityFramework.Guardian.UnitTests.Models
{
    class TestRowPermission : IRowPermission
    {
        public AccessTypes AccessType { get; set; }

        public string EntityTypeName { get; set; }

        public string RowIdentifier { get; set; }
    }
}
