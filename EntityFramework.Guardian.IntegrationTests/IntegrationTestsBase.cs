// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Extensions;
using EntityFramework.Guardian.IntegrationTests.Database;

namespace EntityFramework.Guardian.IntegrationTests
{
    public abstract class IntegrationTestsBase
    {
        protected TestDbContext InitDataContext(GuardianKernel kernel)
        {
            var dbConnection = Effort.DbConnectionFactory.CreateTransient();
            var dataContext = new TestDbContext(dbConnection);

            dataContext.GuardBy(kernel);

            kernel.EnableGuards = false;

            SeedDatabase(dataContext);

            kernel.EnableGuards = true;

            dataContext.DetachAllEntries();

            return dataContext;
        }

        protected abstract void SeedDatabase(TestDbContext dataContext);
    }
}
