// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Hooking;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EntityFramework.Guardian.Extensions
{
    /// <summary>
    /// <see cref="DbContext"/> extensions
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
            kernel.TryValidate();

            kernel.DbContext = dbContext;

            var contextHooker = new DbContextHooker(dbContext, kernel);
            contextHooker.RegisterHooks();

            return dbContext;
        }

        /// <summary>
        /// Detaches all entries.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void DetachAllEntries(this DbContext context)
        {
            foreach (DbEntityEntry dbEntityEntry in (context as DbContext).ChangeTracker.Entries())
            {
                if (dbEntityEntry.Entity != null)
                {
                    dbEntityEntry.State = EntityState.Detached;
                }
            }
        }
    }
}
