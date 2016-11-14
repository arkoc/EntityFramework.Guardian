using EntityFramework.Guardian.Extensions;
using EntityFramework.Guardian.Guards;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace EntityFramework.Guardian.Hooking
{
    /// <summary>
    /// Class for hooking <see cref="DbContext"/>
    /// </summary>
    internal class DbContextHooker
    {
        private readonly DbContext _context;
        private readonly GuardianKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextHooker"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="kernel">The guardian kernel.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
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

        /// <summary>
        /// Registers the hooks.
        /// </summary>
        public void RegisterHooks()
        {
            ObjectContext context = ((IObjectContextAdapter)_context).ObjectContext;

            // Registering events will prevent GC from cleaning DbContextHooker even if
            // there will not be strong reference to DbContextHooker object
            context.ObjectMaterialized += Context_ObjectMaterialized;
            context.SavingChanges += Context_SavingChanges;

        }

        /// <summary>
        /// Handles the SavingChanges event of the Context control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Context_SavingChanges(object sender, EventArgs e)
        {

            if (_kernel.EnableGuards == false)
            {
                return;
            }

            ObjectContext context = ((IObjectContextAdapter)_context).ObjectContext;

            var entries = context.GetModifiedEntries();

            foreach (var entry in entries)
            {
                if (entry == null)
                {
                    continue;
                }

                var protectionContext = new ModifyGuardContext()
                {
                    Entry = entry,
                    AffectedProperties = context.GetAffectedProperties(entry.Entity)
                };

                _kernel.Guards.ModifyGuard.Protect(protectionContext);
            }

        }

        /// <summary>
        /// Handles the ObjectMaterialized event of the Context control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ObjectMaterializedEventArgs"/> instance containing the event data.</param>
        private void Context_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {

            if (_kernel.EnableGuards == false)
            {
                return;
            }

            ObjectContext context = ((IObjectContextAdapter)_context).ObjectContext;

            IObjectAccessEntry objectAccessEntry;
            if (context.TryGetMaterializedEntry(e.Entity, out objectAccessEntry))
            {
                var protectionContext = new RetrieveGuardContext()
                {
                    Entry = objectAccessEntry
                };
                _kernel.Guards.RetrieveGuard.Protect(protectionContext);
            }
        }
    }
}
