using System.Collections.Generic;

namespace EntityFramework.Guardian.Guards
{
    /// <summary>
    /// 
    /// </summary>
    public class ModifyGuardContext
    {
        /// <summary>
        /// Gets or sets the entry.
        /// </summary>
        /// <value>
        /// The entry.
        /// </value>
        public IObjectAccessEntry Entry { get; set; }

        /// <summary>
        /// Gets or sets the affected properties.
        /// </summary>
        /// <value>
        /// The affected properties.
        /// </value>
        public List<string> AffectedProperties { get; set; } = new List<string>();
    }
}
