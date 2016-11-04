using EntityFramework.Guardian.Entities;
using System.Collections.Generic;

namespace EntityFramework.Guardian.UnitTests.Models
{
    class TestPermission : IPermission<TestColumnRestriction>
    {
        public AccessTypes AccessType { get; set; }
        public string EntityTypeName { get; set; }
        public ICollection<TestColumnRestriction> ColumnRestrictions { get; set; } = new List<TestColumnRestriction>();
    }
}
