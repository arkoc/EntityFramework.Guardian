using EntityFramework.Guardian.Models;
using System.Collections.Generic;

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Context used in <see cref="IModifyProtectionPolicy.Check(ModifyPolicyContext, GuardianKernel)"/>
    /// </summary>
    public class ModifyPolicyContext
    {
        /// <summary>
        /// Gets or sets the name of the entity type.
        /// </summary>
        /// <value>
        /// The name of the entity type.
        /// </value>
        public string EntityTypeName { get; set; }

        /// <summary>
        /// Gets or sets the entity row key.
        /// </summary>
        /// <value>
        /// The entity row key.
        /// </value>
        public string EntityRowKey { get; set; }

        /// <summary>
        /// Gets or sets the type of the access.
        /// </summary>
        /// <value>
        /// The type of the access.
        /// </value>
        public AccessTypes AccessType { get; set; }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public IProtectableObject Entity { get; set; }

        /// <summary>
        /// Gets or sets the modified properties.
        /// </summary>
        /// <value>
        /// The modified properties.
        /// </value>
        public List<string> ModifiedProperties { get; set; } = new List<string>();
    }
}
