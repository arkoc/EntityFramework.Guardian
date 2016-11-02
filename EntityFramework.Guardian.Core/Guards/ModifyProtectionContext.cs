using System.Collections.Generic;

namespace EntityFramework.Guardian.Guards
{
    /// <summary>
    /// 
    /// </summary>
    public class ModifyProtectionContext
    {
        /// <summary>
        /// Gets or sets the entry.
        /// </summary>
        /// <value>
        /// The entry.
        /// </value>
        public IObjectAccessEntry Entry { get; set; }

        /// <summary>
        /// Gets or sets the modified properties.
        /// </summary>
        /// <value>
        /// The modified properties.
        /// </value>
        public List<string> ModifiedProperties { get; set; } = new List<string>();
    }
}
