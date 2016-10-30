namespace EntityFramework.Guardian.Core.Protection
{
    public interface IModifyProtector
    {
        void Protect(ModifyProtectionContext context);
    }
}

