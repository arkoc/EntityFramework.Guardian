﻿// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Tests.Models;
using System.Data.Common;
using System.Data.Entity;

namespace EntityFramework.Guardian.IntegrationTests.Database
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbConnection connection) : base(connection, true)
        {
        }

        public DbSet<Model1> Model1s { get; set; }
        public DbSet<Model2> Model2s { get; set; }
    }
}