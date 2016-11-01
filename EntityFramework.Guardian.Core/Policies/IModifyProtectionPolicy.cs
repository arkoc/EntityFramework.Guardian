namespace EntityFramework.Guardian.Policies
{
    public interface IModifyProtectionPolicy
    {
        ModifyPolicyResult Check(ModifyPolicyContext context, GuardianKernel kernel);
    }
}
