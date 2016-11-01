using EntityFramework.Guardian.Policies;

namespace EntityFramework.Guardian.UnitTests.Dummy
{
    public class DummyRetrievePolicy : IRetrieveProtectionPolicy
    {
        public RetrievePolicyResult Check(RetrievePolicyContext context, GuardianKernel kernel)
        {
            return new RetrievePolicyResult(isSuccess: false);
        }
    }
}
