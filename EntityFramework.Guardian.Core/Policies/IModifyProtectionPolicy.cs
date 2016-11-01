namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Modify Policy interface
    /// </summary>
    public interface IModifyProtectionPolicy
    {
        /// <summary>
        /// Checks the policy by specified context and kernel.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="kernel">The kernel.</param>
        /// <returns><see cref="ModifyPolicyResult"/> of policy checking.</returns>
        ModifyPolicyResult Check(ModifyPolicyContext context, GuardianKernel kernel);
    }
}
