namespace EntityFramework.Guardian.Core.Policies
{
    public interface IRetrieveProtectionPolicy
    {
        bool Check(RetrievePolicyContext context, GuardianKernel kernel);
    }
}
