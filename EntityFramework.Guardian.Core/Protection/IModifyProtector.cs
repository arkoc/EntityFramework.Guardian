namespace EntityFramework.Guardian.Protection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModifyProtector
    {
        /// <summary>
        /// Protects by the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        void Protect(ModifyProtectionContext context);
    }
}

