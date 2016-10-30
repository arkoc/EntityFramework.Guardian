namespace EntityFramework.Guardian.Protection
{
    public interface IRetrieveProtector
    {
        void Protect(RetrieveProtectionContext context);
    }
}
