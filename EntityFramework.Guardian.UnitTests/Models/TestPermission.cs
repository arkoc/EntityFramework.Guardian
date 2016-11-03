using EntityFramework.Guardian.Entities;
using System.Collections.Generic;

namespace EntityFramework.Guardian.UnitTests.Models
{
    class TestPermission : IPermission
    {
        public AccessTypes AccessType { get; set; }
        public string EntityTypeName { get; set; }
        public ICollection<IColumnRestriction> ColumnRestrictions { get; set; } = new List<IColumnRestriction>();
    }
}
