using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Guardian
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
        [NotMapped]
        ProtectionResults ProtectionResult { get; set; }

        /// <summary>
        /// Gets or sets the protected properties.
        /// </summary>
        /// <value>
        /// The protected properties.
        /// </value>
        [NotMapped]
        List<string> ProtectedProperties { get; set; }
    }
}
