namespace EntityFramework.Guardian.Policies
{
    public interface IModifyProtectionPolicy
    {
        bool Check(ModifyPolicyContext context, GuardianKernel kernel);
    }
}
