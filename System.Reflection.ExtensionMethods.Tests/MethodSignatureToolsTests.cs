/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Reflection.ExtensionMethods.Tests
{
    [TestClass]
    public class Requirements
    {
        protected MethodInfo GetRequirement (string name) {
            var type = typeof(Requirement);
            var method = type.GetMethod(name);

            return method;
        }

        [TestMethod]
        public void Example_Signature() {
            var type = typeof(Requirements);
            var method = type.GetMethod(nameof(TestMethod));

            var signature = method.GetSignature(false);

            Assert.AreEqual("public string TestMethod(string firstParam)", signature);
        }

        [TestMethod]
        public void Example_Signature_Invokable() {
            var type = typeof(Requirements);
            var method = type.GetMethod(nameof(TestMethod));

            var signature = method.GetSignature(true);

            Assert.AreEqual("TestMethod(firstParam)", signature);
        }

        [TestMethod]
        public void Example_Signature_Generics() {
            var type  = typeof(Requirements);
            var method = type.GetMethod(nameof(TestMethod2));

            var signature = method.GetSignature(false);

            Assert.AreEqual("public System.Collections.Generic.List<string> TestMethod2<T>(string firstParam, T secondParam)", signature);
        }

        [TestMethod]
        public void Example_Signature_Generics_Invokable() {
            var type  = typeof(Requirements);
            var method = type.GetMethod(nameof(TestMethod2));

            var signature = method.GetSignature(true);

            Assert.AreEqual("TestMethod2<T>(firstParam, secondParam)", signature);
        }

        [TestMethod]
        public void Example_Signature_Generics_Nested() {
            var type = typeof(Requirements);
            var method = type.GetMethod(nameof(TestMethod3));

            var signature = method.GetSignature(false);

            Assert.AreEqual("public void TestMethod3(System.Action<System.Action<System.Action<string>>> firstParam)", signature);
        }

        [TestMethod]
        public void Example_Signature_Nullable() {
            var type = typeof(Requirements);
            var method = type.GetMethod(nameof(TestMethod4));

            var signature = method.GetSignature(false);

            Assert.AreEqual("public void TestMethod4(int? firstParam)", signature);
        }

        [TestMethod]
        public void Example_Signature_Accessor() {
            var type = typeof(Requirements);
            var method = type.GetMethod(nameof(TestMethod5), BindingFlags.Static | BindingFlags.NonPublic);

            var signature = method.GetSignature(false);

            Assert.AreEqual("internal static void TestMethod5()", signature);
        }

        [TestMethod]
        public void Example_Signature_ExtensionMethod() {
            var type = typeof(TestExtensionMethods);
            var method = type.GetMethod(nameof(TestExtensionMethods.ExtensionMethod));

            var signature = method.GetSignature(false);

            Assert.AreEqual("public static string ExtensionMethod(this string firstParam, bool secondParam)", signature);
        }

        [TestMethod]
        public void Example_Signature_ExtensionMethod_Invokable() {
            var type = typeof(TestExtensionMethods);
            var method = type.GetMethod(nameof(TestExtensionMethods.ExtensionMethod));

            var signature = method.GetSignature(true);

            Assert.AreEqual("ExtensionMethod(secondParam)", signature);
        }

        public string TestMethod(string firstParam) {
            throw new NotImplementedException();
        }

        public List<string> TestMethod2<T>(string firstParam, T secondParam) {
            throw new NotImplementedException();
        }

        public void TestMethod3(System.Action<System.Action<System.Action<string>>> firstParam) {
            throw new NotImplementedException();
        }

        public void TestMethod4(Nullable<int> firstParam) {
            throw new NotImplementedException();
        }

        internal static void TestMethod5() {

        }
    }

    public static class TestExtensionMethods
    {
        public static string ExtensionMethod(this string firstParam, bool secondParam) {
            throw new NotImplementedException();
        }
    }
}
