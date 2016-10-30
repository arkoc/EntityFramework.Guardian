namespace EntityFramework.Guardian.Protection
{
    public interface IModifyProtector
    {
        void Protect(ModifyProtectionContext context);
    }
}

