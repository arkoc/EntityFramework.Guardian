using System;
using EntityFramework.Guardian.Policies;

namespace EntityFramework.Guardian.UnitTests.Dummy
{
    public class DummyModifyPolicy : IModifyPolicy
    {
        public ModifyPolicyResult Check(ModifyPolicyContext context)
        {
            return new ModifyPolicyResult(isSuccess: false);
        }
    }
}
