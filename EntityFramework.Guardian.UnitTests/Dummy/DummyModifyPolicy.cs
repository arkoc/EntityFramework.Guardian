using System;
using EntityFramework.Guardian.Policies;

namespace EntityFramework.Guardian.UnitTests.Dummy
{
    public class DummyModifyPolicy : IModifyProtectionPolicy
    {
        public bool Check(ModifyPolicyContext context, GuardianKernel kernel)
        {
            return false;
        }
    }
}
