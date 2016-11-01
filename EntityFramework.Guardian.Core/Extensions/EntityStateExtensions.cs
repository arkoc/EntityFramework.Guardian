using EntityFramework.Guardian.Models;
using System.Data.Entity;

namespace EntityFramework.Guardian.Extensions
{
    /// <summary>
    /// <see cref="EntityState"/> Extensions
    /// </summary>
    internal static class EntityStateExtensions
    {
        /// <summary>
        /// Gets the type of the access.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>Mapped AccessType of EntityState</returns>
        internal static AccessTypes GetAccessType(this EntityState state)
        {
            var accessType = AccessTypes.Get;

            if (state.HasFlag(EntityState.Deleted))
            {
                accessType = AccessTypes.Delete;
            }

            if (state.HasFlag(EntityState.Modified))
            {
                accessType = AccessTypes.Update;
            }

            if (state.HasFlag(EntityState.Added))
            {
                accessType = AccessTypes.Add;
            }

            return accessType;
        }
    }
}
