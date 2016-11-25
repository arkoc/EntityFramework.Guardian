using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Extensions;
using System.Data.Entity;
using Xunit;

namespace EntityFramework.Guardian.UnitTests
{
    public class EntityStateExtensionsTests
    {
        [Fact]
        public void GetActionType_ShouldReturnGet()
        {
            var entityState = EntityState.Unchanged;
            var actionType = entityState.GetActionType();
            Assert.Equal(ActionTypes.Get, actionType);
        }

        [Fact]
        public void GetActionType_ShouldReturnAdd()
        {
            var entityState = EntityState.Added;
            var actionType = entityState.GetActionType();
            Assert.Equal(ActionTypes.Add, actionType);
        }

        [Fact]
        public void GetActionType_ShouldReturnUpdate()
        {
            var entityState = EntityState.Modified;
            var actionType = entityState.GetActionType();
            Assert.Equal(ActionTypes.Update, actionType);
        }

        [Fact]
        public void GetActionType_ShouldReturnDelete()
        {
            var entityState = EntityState.Deleted;
            var actionType = entityState.GetActionType();
            Assert.Equal(ActionTypes.Delete, actionType);
        }
    }
}
