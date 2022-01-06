/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Linq;
using System.Reflection;

namespace Eltons.ReflectionKit
{
    public abstract class MethodBaseSignature
    {
        public string BuildAccessor(MethodBase method)
        {
            string signature = null;

            if (method.IsAssembly)
            {
                signature = "internal ";

                if (method.IsFamily)
                    signature += "protected ";
            }
            else if (method.IsPublic)
            {
                signature = "public ";
            }
            else if (method.IsPrivate)
            {
                signature = "private ";
            }
            else if (method.IsFamily)
            {
                signature = "protected ";
            }

            if (method.IsStatic)
                signature += "static ";

            return signature;
        }

        public string BuildArguments(MethodBase method, bool invokable)
        {
            var isExtensionMethod = method.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false);
            var methodParameters = method.GetParameters().AsEnumerable();

            // If this signature is designed to be invoked and it's an extension method
            if (isExtensionMethod && invokable)
            {
                // Skip the first argument
                methodParameters = methodParameters.Skip(1);
            }

            var methodParameterSignatures = methodParameters.Select(param =>
            {
                var signature = string.Empty;

                if (param.ParameterType.IsByRef)
                    signature = "ref ";
                else if (param.IsOut)
                    signature = "out ";
                else if (isExtensionMethod && param.Position == 0)
                    signature = "this ";

                if (!invokable)
                {
                    signature += TypeSignature.Build(param.ParameterType) + " ";
                }

                signature += param.Name;

                return signature;
            });

            var methodParameterString = "(" + string.Join(", ", methodParameterSignatures) + ")";

            return methodParameterString;
        }

        public string BuildGenerics(MethodBase method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (!method.IsGenericMethod) throw new ArgumentException($"{method.Name} is not generic.");

            return TypeSignature.BuildGenerics(method.GetGenericArguments());
        }
    }
}