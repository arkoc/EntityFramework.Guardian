namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Modify Policy interface
    /// </summary>
    public interface IModifyPolicy
    {
        /// <summary>
        /// Checks the policy by specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><see cref="ModifyPolicyResult"/> of policy checking.</returns>
        ModifyPolicyResult Check(ModifyPolicyContext context);
    }
}
