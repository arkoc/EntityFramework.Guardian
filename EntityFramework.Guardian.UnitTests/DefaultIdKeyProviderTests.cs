// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using EntityFramework.Guardian.Tests.Models;
using Xunit;
using EntityFramework.Guardian.Services;

namespace EntityFramework.Guardian.UnitTests
{
    public class DefaultIdKeyProviderTests
    {
        private IEntityKeyProvider _defaultIdKeyProvider;

        public DefaultIdKeyProviderTests()
        {
            _defaultIdKeyProvider = new DefaultIdKeyProvider();
        }

        [Fact]
        public void ModelWithIntKey_ShouldSuccess()
        {
            int entityKey = 1;
            var entity = new Model1()
            {
                Id = entityKey
            };

            var key = _defaultIdKeyProvider.GetKey(entity);

            Assert.Equal(entityKey.ToString(), key);
        }

        [Fact]
        public void ModelWithStringKey_ShouldSuccess()
        {
            string entityKey = "1";
            var entity = new Model2()
            {
                Id = entityKey
            };

            var key = _defaultIdKeyProvider.GetKey(entity);

            Assert.Equal(entityKey, key);
        }


        [Fact]
        public void ModelWithNoIdProperty_ShouldFail()
        {
            var entity = new Model3();
            Assert.Throws(typeof(InvalidOperationException), () => { _defaultIdKeyProvider.GetKey(entity); });
        }
    }
}
