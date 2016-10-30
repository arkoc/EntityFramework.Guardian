using EntityFramework.Guardian.Configuration;
using EntityFramework.Guardian.Policies;
using EntityFramework.Guardian.Protection;
using System.Collections.Generic;

namespace EntityFramework.Guardian
{
    public class GuardianKernel
    {
        public IEntityKeyProvider EntityKeyProvider { get; private set; }

        public IModifyProtector ModifyProtector { get; private set; }
        public IRetrieveProtector RetrieveProtector { get; private set; }

        public List<IModifyProtectionPolicy> ModifyProtectionPolicies { get; private set; }
        public List<IRetrieveProtectionPolicy> RetrieveProtectionPolicies { get; private set; }

        public GuardianPermissions Permissions { get; set; }

        public GuardianKernel()
        {
            EntityKeyProvider = new DefaultIdKeyProvider();

            ModifyProtector = new DefaultModifyProtector(this);
            RetrieveProtector = new DefaultRetrieveProtector(this);

            ModifyProtectionPolicies = new List<IModifyProtectionPolicy>();
            RetrieveProtectionPolicies = new List<IRetrieveProtectionPolicy>();

            ModifyProtectionPolicies.Add(new DefaultModifyPolicy());
            RetrieveProtectionPolicies.Add(new DefaultRetrievePolicy());

            Permissions = new GuardianPermissions();
        }
    }
}
