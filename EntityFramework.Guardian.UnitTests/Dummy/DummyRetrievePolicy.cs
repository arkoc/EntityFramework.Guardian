using EntityFramework.Guardian.Policies;

namespace EntityFramework.Guardian.UnitTests.Dummy
{
    public class DummyRetrievePolicy : IRetrieveProtectionPolicy
    {
        public bool Apply(RetrievePolicyContext context, GuardianKernel kernel)
        {
            context.Entity.ProtectionResult = Guardian.Models.ProtectionResults.Deny;
            return true;
        }
    }
}
