namespace EntityFramework.Guardian.Core.Protection
{
    public interface IRetrieveProtector
    {
        void Protect(RetrieveProtectionContext context);
    }
}
