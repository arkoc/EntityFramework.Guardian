namespace EntityFramework.Guardian.Configuration
{
    public interface IEntityKeyProvider
    {
        string GetKey(object entity);
    }
}
