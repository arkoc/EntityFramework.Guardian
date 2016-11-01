using EntityFramework.Guardian.Models;

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Context used in <see cref="IRetrieveProtectionPolicy.Check(RetrievePolicyContext, GuardianKernel)"/>
    /// </summary>
    public class RetrievePolicyContext
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
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public IProtectableObject Entity { get; set; }
    }
}
