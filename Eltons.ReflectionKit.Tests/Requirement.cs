/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Reflection;

namespace Eltons.ReflectionKit.Tests
{
    public class Requirement
    {
        [Requirement("public void CanReturnVoid()", "CanReturnVoid()")]
        public void CanReturnVoid() {

        }
    }

    public static class RequirmentExtensionMethods
    {
        public static RequirementAttribute GetRequirement(this MethodInfo method) {
            return method.GetCustomAttribute<RequirementAttribute>();
        }
    }

    [System.AttributeUsage(AttributeTargets.Method,
        Inherited = false,
        AllowMultiple = false)]
    public sealed class RequirementAttribute : Attribute
    {
        public RequirementAttribute(string expectedSignature, string expectedInvokeableSignature) {
            ExpectedSignature = expectedSignature;
            ExpectedInvokeableSignature = expectedInvokeableSignature;
        }

        public string ExpectedSignature { get; set; }
        public string ExpectedInvokeableSignature { get; set; }
    }
}
