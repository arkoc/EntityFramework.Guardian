namespace EntityFramework.Guardian.Core.Policies
{
    public interface IModifyProtectionPolicy
    {
        bool Check(ModifyPolicyContext context, GuardianKernel kernel);
    }
}
