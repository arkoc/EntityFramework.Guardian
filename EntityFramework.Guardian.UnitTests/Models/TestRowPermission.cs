using System.Collections.Generic;
using EntityFramework.Guardian.Entities;

namespace EntityFramework.Guardian.UnitTests.Models
{
    class TestRowPermission : IRowPermission<TestColumnRestriction>
    {
        public AccessTypes AccessType { get; set; }


        public string EntityTypeName { get; set; }

        public string RowIdentifier { get; set; }
        public ICollection<TestColumnRestriction> ColumnRestrictions { get; set; } = new List<TestColumnRestriction>();
    }
}
