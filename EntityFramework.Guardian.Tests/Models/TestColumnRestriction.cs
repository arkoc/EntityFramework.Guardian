using EntityFramework.Guardian.Core.Models;

namespace EntityFramework.Guardian.Tests.Models
{
    class TestColumnRestriction : IColumnRestriction
    {
        public AccessTypes AccessType { get; set; }
        public string EntityTypeName { get; set; }
        public string PropertyName { get; set; }
    }
}
