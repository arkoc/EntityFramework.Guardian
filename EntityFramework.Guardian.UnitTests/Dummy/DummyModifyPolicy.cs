using EntityFramework.Guardian.Policies;

namespace EntityFramework.Guardian.UnitTests.Dummy
{
    public class DummyModifyPolicy : IModifyProtectionPolicy
    {
        public ModifyPolicyResult Check(ModifyPolicyContext context, GuardianKernel kernel)
        {
            return new ModifyPolicyResult(isSuccess: false);
        }
    }
}
