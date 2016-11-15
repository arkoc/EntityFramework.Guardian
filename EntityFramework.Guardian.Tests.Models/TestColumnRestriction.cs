// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Entities;

namespace EntityFramework.Guardian.Tests.Models
{
    public class TestColumnRestriction : IColumnRestriction
    {
        public string PropertyName { get; set; }
    }
}
