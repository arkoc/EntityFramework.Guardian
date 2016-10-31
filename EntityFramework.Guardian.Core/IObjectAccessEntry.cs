using EntityFramework.Guardian.Models;

namespace EntityFramework.Guardian
{
    public interface IObjectAccessEntry
    {
        IProtectableObject Entity { get; }
        AccessTypes AccessType { get; }
    }
}
