using EntityFramework.Guardian.Entities;

namespace EntityFramework.Guardian.UnitTests.Models
{
    class TestColumnRestriction : IColumnRestriction
    {
        public string PropertyName { get; set; }
    }
}
