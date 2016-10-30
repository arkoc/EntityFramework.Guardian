using EntityFramework.Guardian.Core.Models;

namespace EntityFramework.Guardian.Tests.Models
{
    class TestRowPermission : IRowPermission
    {
        public AccessTypes AccessType { get; set; }

        public string EntityTypeName { get; set; }

        public string RowIdentifier { get; set; }
    }
}
