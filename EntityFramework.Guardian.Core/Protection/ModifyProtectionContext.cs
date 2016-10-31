using System.Collections.Generic;

namespace EntityFramework.Guardian.Protection
{
    public class ModifyProtectionContext
    {
        public IObjectAccessEntry Entry { get; set; }
        public List<string> ModifiedProperties { get; set; } = new List<string>();
    }
}
