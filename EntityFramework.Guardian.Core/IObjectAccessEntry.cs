using EntityFramework.Guardian.Models;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// 
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
        AccessTypes AccessType { get; }
    }
}
