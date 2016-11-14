using EntityFramework.Guardian.Configuration;
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
