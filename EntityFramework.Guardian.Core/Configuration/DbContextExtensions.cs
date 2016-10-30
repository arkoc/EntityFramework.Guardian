using EntityFramework.Guardian.Core.Hooking;
using System.Data.Entity;

namespace EntityFramework.Guardian.Core.Configuration
{
    public static class DbContextExtensions
    {
        public static DbContext GuardBy(this DbContext dbContext, GuardianKernel kernel)
        {
            var contextHooker = new DbContextHooker(dbContext, kernel);
            contextHooker.RegisterHooks();

            return dbContext;
        }
    }
}
