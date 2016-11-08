using EntityFramework.Guardian.Policies;
using System.Collections.Generic;

namespace EntityFramework.Guardian
{
    /// <summary>
    /// Guardian Kernel Policies
    /// </summary>
    public class KernelPolicies
    {
        /// <summary>
        /// Gets the modify protection policies.
        /// </summary>
        /// <value>
        /// The modify protection policies.
        /// </value>
        public List<IModifyPolicy> ModifyPolicies { get; private set; } = new List<IModifyPolicy>();

        /// <summary>
        /// Gets the retrieve protection policies.
        /// </summary>
        /// <value>
        /// The retrieve protection policies.
        /// </value>
        public List<IRetrievePolicy> RetrievePolicies { get; private set; } = new List<IRetrievePolicy>();
    }
}
