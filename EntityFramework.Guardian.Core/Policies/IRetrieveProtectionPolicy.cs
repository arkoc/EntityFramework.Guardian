namespace EntityFramework.Guardian.Policies
{
    public interface IRetrieveProtectionPolicy
    {
        bool Apply(RetrievePolicyContext context, GuardianKernel kernel);
    }
}
