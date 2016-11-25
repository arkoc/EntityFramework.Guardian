// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Extensions;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace EntityFramework.Guardian.Hooking
{
    internal class DbContextHooker
    {
        private readonly DbContext _context;
        private readonly GuardianKernel _kernel;

        public DbContextHooker(DbContext context, GuardianKernel kernel)
        {
            Ensure.NotNull(context, nameof(context));
            Ensure.NotNull(kernel, nameof(kernel));

            _context = context;
            _kernel = kernel;
        }

        public void RegisterHooks()
        {
            ObjectContext context = ((IObjectContextAdapter)_context).ObjectContext;

            // Registering events will prevent GC from cleaning DbContextHooker even if
            // there will not be strong reference to DbContextHooker object
            context.ObjectMaterialized += Context_ObjectMaterialized;
            context.SavingChanges += Context_SavingChanges;
        }

        private void Context_SavingChanges(object sender, EventArgs e)
        {
            if (_kernel.EnableGuards == false)
            {
                return;
            }

            ObjectContext objectContext = ((IObjectContextAdapter)_context).ObjectContext;

            var entries = objectContext.GetModifiedEntries();

            foreach (var entry in entries)
            {
                if (entry == null)
                {
                    continue;
                }

                var protectionContext = new SubmitProtectionContext()
                {
                    Kernel = _kernel,
                    EntityType = entry.Entity.GetType(),
                    Entry = entry,
                    LocalValues = objectContext.GetModifiedLocalValues(entry.Entity),
                    OriginalValues = objectContext.GetModifiedOriginalValues(entry.Entity)
                };

                _kernel.SubmitGuard.Protect(protectionContext);
            }
        }

        private void Context_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            if (_kernel.EnableGuards == false)
            {
                return;
            }

            ObjectContext objectContext = ((IObjectContextAdapter)_context).ObjectContext;

            IObjectAccessEntry objectAccessEntry;
            if (objectContext.TryGetMaterializedEntry(e.Entity, out objectAccessEntry))
            {
                var protectionContext = new QueryProtectionContext()
                {
                    Kernel = _kernel,
                    Entry = objectAccessEntry,
                    EntityType = e.Entity.GetType()
                };

                _kernel.QueryGuard.Protect(protectionContext);
            }
        }
    }
}
