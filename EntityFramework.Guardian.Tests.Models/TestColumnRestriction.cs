using EntityFramework.Guardian.Entities;

namespace EntityFramework.Guardian.Tests.Models
{
    public class TestColumnRestriction : IColumnRestriction
    {
        public string PropertyName { get; set; }
    }
}
