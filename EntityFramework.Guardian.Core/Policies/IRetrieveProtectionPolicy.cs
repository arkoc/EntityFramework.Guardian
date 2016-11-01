namespace EntityFramework.Guardian.Policies
{
    public interface IRetrieveProtectionPolicy
    {
        RetrievePolicyResult Check(RetrievePolicyContext context, GuardianKernel kernel);
    }
}
