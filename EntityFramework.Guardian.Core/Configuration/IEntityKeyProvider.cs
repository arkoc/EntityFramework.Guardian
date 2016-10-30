namespace EntityFramework.Guardian.Core.Configuration
{
    public interface IEntityKeyProvider
    {
        string GetKey(object entity);
    }
}
