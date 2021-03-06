﻿using EntityFramework.Guardian.Extensions;
using System;
using Xunit;

namespace EntityFramework.Guardian.UnitTests
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void GetDefaultValue_ShouldReturnDefaultValue()
        {
            DateTime dt = default(DateTime);
            var defaultValue = dt.GetType().GetDefaultValue();

            Assert.Equal(dt, defaultValue);
        }

        [Fact]
        public void GetDefaultValue_ShouldReturnNull()
        {
            var defaultValue = typeof(string).GetDefaultValue();

            Assert.Equal(null, defaultValue);
        }
    }
}
