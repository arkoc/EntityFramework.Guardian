using EntityFramework.Guardian.Hooking;
using System;
using System.Data.Entity;

namespace EntityFramework.Guardian.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Guards the <see cref="DbContext"/>  by <see cref="GuardianKernel"/> .
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="kernel">The guardian kernel.</param>
        public static DbContext GuardBy(this DbContext dbContext, GuardianKernel kernel)
        {
            if (kernel.DbContext != null)
            {
                throw new InvalidOperationException("Specified GuardianKernel object already associcated with another DbContext.");
            }

            kernel.DbContext = dbContext;

            var contextHooker = new DbContextHooker(dbContext, kernel);
            contextHooker.RegisterHooks();

            return dbContext;
        }
    }
}
