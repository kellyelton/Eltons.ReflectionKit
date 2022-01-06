/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;

namespace Eltons.ReflectionKit
{
    public static class TypeExtensionMethods
    {
        /// <summary>
        /// Is this type a generic type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>True if generic, otherwise False</returns>
        public static bool IsGeneric(this Type type)
        {
            return type.IsGenericType
                && type.Name.Contains("`");//TODO: Figure out why IsGenericType isn't good enough and document (or remove) this condition
        }

        public static bool IsNullable(this Type type, out Type underlyingType)
        {
            underlyingType = Nullable.GetUnderlyingType(type);
            return underlyingType != null;
        }
    }
}