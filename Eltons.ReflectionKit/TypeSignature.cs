/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Eltons.ReflectionKit
{
    public static class TypeSignature
    {
        /// <summary>
        /// Get a fully qualified signature for <paramref name="type"/>
        /// </summary>
        /// <param name="type">Type. May be generic or <see cref="Nullable{T}"/></param>
        /// <returns>Fully qualified signature</returns>
        public static string Build(Type type) {
            var isNullableType = type.IsNullable(out var underlyingNullableType);

            var signatureType = isNullableType
                ? underlyingNullableType
                : type;

            var isGenericType = signatureType.IsGeneric();

            var signature = GetQualifiedTypeName(signatureType);

            if (isGenericType) {
                // Add the generic arguments
                signature += BuildGenerics(signatureType.GetGenericArguments());
            }

            if (isNullableType) {
                signature += "?";
            }

            return signature;
        }

        /// <summary>
        /// Takes an <see cref="IEnumerable{T}"/> and creates a generic type signature (&lt;string, string&gt; for example)
        /// </summary>
        /// <param name="genericArgumentTypes"></param>
        /// <returns>Generic type signature like &lt;Type, ...&gt;</returns>
        public static string BuildGenerics(IEnumerable<Type> genericArgumentTypes) {
            var argumentSignatures = genericArgumentTypes.Select(Build);

            return "<" + string.Join(", ", argumentSignatures) + ">";
        }

        /// <summary>
        /// Gets the fully qualified type name of <paramref name="type"/>.
        /// This will use any keywords in place of types where possible (string instead of System.String for example)
        /// </summary>
        /// <param name="type"></param>
        /// <returns>The fully qualified name for <paramref name="type"/></returns>
        public static string GetQualifiedTypeName(Type type) {
            switch (type.Name) {
                case "String":
                    return "string";
                case "Int32":
                    return "int";
                case "Decimal":
                    return "decimal";
                case "Object":
                    return "object";
                case "Void":
                    return "void";
                case "Boolean":
                    return "bool";
            }

            //TODO: Figure out how type.FullName could be null and document (or remove) this conditional
            var signature = string.IsNullOrWhiteSpace(type.FullName)
                ? type.Name
                : type.FullName;

            if(type.IsGeneric())
                signature = RemoveGenericTypeNameArgumentCount(signature);

            return signature;
        }


        /// <summary>
        /// This removes the `{argumentcount} from a the signature of a generic type
        /// </summary>
        /// <param name="genericTypeSignature">Signature of a generic type</param>
        /// <returns><paramref name="genericTypeSignature"/> without any argument count</returns>
        public static string RemoveGenericTypeNameArgumentCount(string genericTypeSignature) {
            return genericTypeSignature.Substring(0, genericTypeSignature.IndexOf('`'));
        }
    }
}
