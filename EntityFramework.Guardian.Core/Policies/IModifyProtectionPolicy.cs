namespace EntityFramework.Guardian.Policies
{
    public interface IModifyProtectionPolicy
    {
        bool Apply(ModifyPolicyContext context, GuardianKernel kernel);
    }
}
