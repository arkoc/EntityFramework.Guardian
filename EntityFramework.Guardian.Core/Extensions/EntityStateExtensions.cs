using EntityFramework.Guardian.Models;
using System.Data.Entity;

namespace EntityFramework.Guardian.Extensions
{
    internal static class EntityStateExtensions
    {
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
