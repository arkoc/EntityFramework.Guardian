﻿// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.


using EntityFramework.Guardian.Entities;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// ObjectAccess Adapter
    /// </summary>
    public interface IObjectAccessEntry
    {
        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        IProtectableObject Entity { get; }

        /// <summary>
        /// Gets the type of the access.
        /// </summary>
        /// <value>
        /// The type of the access.
        /// </value>
        ActionTypes ActionType { get; }
    }
}
