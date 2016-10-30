using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityFramework.Guardian.Core.Configuration;
using EntityFramework.Guardian.Tests.Models;

namespace EntityFramework.Guardian.Tests
{
    [TestClass]
    public class DefaultIdKeyProviderTests
    {
        private IEntityKeyProvider defaultIdKeyProvider;

        public DefaultIdKeyProviderTests()
        {
            defaultIdKeyProvider = new DefaultIdKeyProvider();
        }

        [TestMethod]
        public void ModelWithIntKey_ShouldSuccess()
        {
            int entityKey = 1;
            var entity = new Model1()
            {
                Id = entityKey
            };

            var key = defaultIdKeyProvider.GetKey(entity);

            Assert.AreEqual(entityKey.ToString(), key);
        }

        [TestMethod]
        public void ModelWithStringKey_ShouldSuccess()
        {
            string entityKey = "1";
            var entity = new Model2()
            {
                Id = entityKey
            };

            var key = defaultIdKeyProvider.GetKey(entity);

            Assert.AreEqual(entityKey, key);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ModelWithNoIdProperty_ShouldFail()
        {
            var entity = new Model3();
            var key = defaultIdKeyProvider.GetKey(entity);
        }

    }
}
