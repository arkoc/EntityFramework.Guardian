// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Entities;
using System.Collections.Generic;

namespace EntityFramework.Guardian.Tests.Models
{
    public class TestPermission : IPermission<TestColumnRestriction>
    {
        public AccessTypes AccessType { get; set; }
        public string EntityTypeName { get; set; }
        public ICollection<TestColumnRestriction> ColumnRestrictions { get; set; } = new List<TestColumnRestriction>();
    }
}
