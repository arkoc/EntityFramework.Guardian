using EntityFramework.Guardian.Policies;

namespace EntityFramework.Guardian.UnitTests.Dummy
{
    public class DummyRetrievePolicy : IRetrievePolicy
    {
        public RetrievePolicyResult Check(RetrievePolicyContext context, GuardianKernel kernel)
        {
            return new RetrievePolicyResult(isSuccess: false);
        }
    }
}
