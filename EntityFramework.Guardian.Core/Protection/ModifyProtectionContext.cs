using System.Collections.Generic;

namespace EntityFramework.Guardian.Core.Protection
{
    public class ModifyProtectionContext
    {
        public ObjectAccessEntry Entry { get; set; }
        public IEnumerable<string> ModifiedProperties { get; set; } = new List<string>();
    }
}
