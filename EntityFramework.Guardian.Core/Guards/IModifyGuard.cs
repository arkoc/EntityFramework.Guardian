namespace EntityFramework.Guardian.Guards
{
    /// <summary>
    /// Modify Guard Interface
    /// </summary>
    public interface IModifyGuard
    {
        /// <summary>
        /// Protects by the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        void Protect(ModifyGuardContext context);
    }
}

