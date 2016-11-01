namespace EntityFramework.Guardian.Protection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRetrieveProtector
    {
        /// <summary>
        /// Protects by the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        void Protect(RetrieveProtectionContext context);
    }
}
