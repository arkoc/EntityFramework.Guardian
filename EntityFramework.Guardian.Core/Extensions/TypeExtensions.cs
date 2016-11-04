﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Determines whether [is assignable to generic type] [the specified generic type].
        /// </summary>
        /// <param name="givenType">Type of the given.</param>
        /// <param name="genericType">Type of the generic.</param>
        /// <returns>
        ///   <c>true</c> if [is assignable to generic type] [the specified generic type]; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }
    }
}