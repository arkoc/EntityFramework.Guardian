using System.Collections.Generic;

namespace EntityFramework.Guardian.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProtectableObject
    {
        /// <summary>
        /// Gets or sets the protection result.
        /// </summary>
        /// <value>
        /// The protection result.
        /// </value>
        ProtectionResults ProtectionResult { get; set; }

        /// <summary>
        /// Gets or sets the protected properties.
        /// </summary>
        /// <value>
        /// The protected properties.
        /// </value>
        List<string> ProtectedProperties { get; set; }
    }
}
