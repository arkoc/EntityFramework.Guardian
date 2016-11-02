namespace EntityFramework.Guardian.Guards
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRetrieveGuard
    {
        /// <summary>
        /// Protects by the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        void Protect(RetrieveProtectionContext context);
    }
}
