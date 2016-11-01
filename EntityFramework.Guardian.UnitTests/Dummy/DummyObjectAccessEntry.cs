using EntityFramework.Guardian.Entities;

namespace EntityFramework.Guardian.UnitTests.Dummy
{
    public class DummyObjectAccessEntry : IObjectAccessEntry
    {
        public DummyObjectAccessEntry(AccessTypes accessType, IProtectableObject entity)
        {
            AccessType = accessType;
            Entity = entity;
        }

        public AccessTypes AccessType { get; }

        public IProtectableObject Entity { get; }
    }
}
