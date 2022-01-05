/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System.Reflection;
using System.Text;

namespace Eltons.ReflectionKit
{
    public class ConstructorMethodSignature : MethodBaseSignature
    {
        public string Build(ConstructorInfo method, bool invokable)
        {
            var signatureBuilder = new StringBuilder();

            // Add our method accessors if it's not invokable
            if (!invokable)
            {
                signatureBuilder.Append(BuildAccessor(method));
            }

            // Add method name
            signatureBuilder.Append(method.DeclaringType.Name);

            // Add method generics
            if (method.IsGenericMethod)
            {
                signatureBuilder.Append(BuildGenerics(method));
            }

            // Add method parameters
            signatureBuilder.Append(BuildArguments(method, invokable));

            return signatureBuilder.ToString();
        }
    }
}