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
