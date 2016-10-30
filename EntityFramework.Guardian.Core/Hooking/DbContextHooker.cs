using EntityFramework.Guardian.Core.Extensions;
using EntityFramework.Guardian.Core.Protection;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace EntityFramework.Guardian.Core.Hooking
{
    internal class DbContextHooker
    {
        private readonly DbContext _context;
        private readonly GuardianKernel _kernel;

        public DbContextHooker(DbContext context, GuardianKernel kernel)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }

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
            ObjectContext context = ((IObjectContextAdapter)_context).ObjectContext;

            var entries = context.GetModifiedEntries();

            foreach (var entry in entries)
            {
                var protectionContext = new ModifyProtectionContext()
                {
                    Entry = entry,
                    ModifiedProperties = context.GetModifiedProperties(entry.Entity)
                };

                _kernel.ModifyProtector.Protect(protectionContext);
            }

        }

        private void Context_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            ObjectContext context = ((IObjectContextAdapter)_context).ObjectContext;

            ObjectAccessEntry objectAccessEntry;
            if (context.TryGetMaterializedEntry(e.Entity, out objectAccessEntry))
            {
                var protectionContext = new RetrieveProtectionContext();
                _kernel.RetrieveProtector.Protect(protectionContext);
            }
        }
    }
}
