namespace EntityFramework.Guardian.Guards
{
    /// <summary>
    /// Retrieve Guard Interface
    /// </summary>
    public interface IRetrieveGuard
    {
        /// <summary>
        /// Protects by the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        void Protect(RetrieveGuardContext context);
    }
}
