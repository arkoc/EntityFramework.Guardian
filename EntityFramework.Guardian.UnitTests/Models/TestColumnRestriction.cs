using EntityFramework.Guardian.Entities;

namespace EntityFramework.Guardian.UnitTests.Models
{
    class TestColumnRestriction : IColumnRestriction
    {
        public AccessTypes AccessType { get; set; }
        public string EntityTypeName { get; set; }
        public string PropertyName { get; set; }
    }
}
