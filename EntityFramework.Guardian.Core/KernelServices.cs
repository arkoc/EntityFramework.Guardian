using EntityFramework.Guardian.Configuration;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// Guardian Kernel Services
    /// </summary>
    public class KernelServices
    {
        /// <summary>
        /// Gets the entity key provider.
        /// </summary>
        /// <value>
        /// The entity key provider.
        /// </value>
        public IEntityKeyProvider EntityKeyProvider { get; set; }
    }
}
