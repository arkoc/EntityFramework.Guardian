using EntityFramework.Guardian.Entities;

namespace EntityFramework.Guardian.UnitTests.Dummy
{
    public class DummyObjectAccessEntry : IObjectAccessEntry
    {
        public DummyObjectAccessEntry(IProtectableObject entity, AccessTypes accessType)
        {
            Entity = entity;
            AccessType = accessType;
        }

        public AccessTypes AccessType { get; }

        public IProtectableObject Entity { get; }
    }
}
