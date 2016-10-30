using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Guardian.Core.Models
{
    public interface IProtectableObject
    {
        [NotMapped]
        ProtectionResults ProtectionResult { get; set; }

        [NotMapped]
        List<string> ProtectedProperties { get; set; }
    }
}
