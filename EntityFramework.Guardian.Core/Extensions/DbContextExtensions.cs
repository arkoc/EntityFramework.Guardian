// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Hooking;
using System.Data.Entity;

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
        /// <param name="dbContext">The database context to Guard.</param>
        /// <param name="kernel">The guardian kernel.</param>
        public static DbContext UseGuardian(this DbContext dbContext, GuardianKernel kernel)
        {
            kernel.TryValidate();

            var contextHooker = new DbContextHooker(dbContext, kernel);
            contextHooker.RegisterHooks();

            return dbContext;
        }
    }
}
