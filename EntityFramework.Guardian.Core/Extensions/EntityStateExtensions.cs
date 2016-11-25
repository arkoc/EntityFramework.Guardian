// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Entities;
using System.Data.Entity;

namespace EntityFramework.Guardian.Extensions
{
    internal static class EntityStateExtensions
    {
        internal static ActionTypes GetActionType(this EntityState state)
        {
            var accessType = ActionTypes.Get;

            if (state.HasFlag(EntityState.Deleted))
            {
                accessType = ActionTypes.Delete;
            }

            if (state.HasFlag(EntityState.Modified))
            {
                accessType = ActionTypes.Update;
            }

            if (state.HasFlag(EntityState.Added))
            {
                accessType = ActionTypes.Add;
            }

            return accessType;
        }
    }
}
