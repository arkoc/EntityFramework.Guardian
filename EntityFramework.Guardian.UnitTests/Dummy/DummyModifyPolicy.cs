using System;
using EntityFramework.Guardian.Policies;

namespace EntityFramework.Guardian.UnitTests.Dummy
{
    public class DummyModifyPolicy : IModifyProtectionPolicy
    {
        public bool Apply(ModifyPolicyContext context, GuardianKernel kernel)
        {
            return false;
        }
    }
}
