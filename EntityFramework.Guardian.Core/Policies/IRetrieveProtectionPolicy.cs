namespace EntityFramework.Guardian.Policies
{
    public interface IRetrieveProtectionPolicy
    {
        bool Check(RetrievePolicyContext context, GuardianKernel kernel);
    }
}
