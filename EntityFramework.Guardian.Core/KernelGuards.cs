using EntityFramework.Guardian.Guards;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// Guardian Kernel Guards
    /// </summary>
    public class KernelGuards
    {
        /// <summary>
        /// Gets the modify protector.
        /// </summary>
        /// <value>
        /// The modify protector.
        /// </value>
        public IModifyGuard ModifyGuard { get; set; }

        /// <summary>
        /// Gets the retrieve protector.
        /// </summary>
        /// <value>
        /// The retrieve protector.
        /// </value>
        public IRetrieveGuard RetrieveGuard { get; set; }
    }
}
