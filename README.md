# System.Reflection.ExtensionMethods
Rewrite of code I posted here https://stackoverflow.com/a/13318056/222054

This is an extension method on `MethodInfo` called `GetSignature(bool invokable)`

For ideas on how to use this, please view the [Examples](https://github.com/kellyelton/System.Reflection.ExtensionMethods/blob/master/System.Reflection.ExtensionMethods.Tests/Examples.cs)

I've added a snipped of the beginning of the class

```cs
/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Reflection.ExtensionMethods.Tests
{
    [TestClass]
    public class Examples
    {
        [TestMethod]
        public void Example_Signature() {
            var type = typeof(Examples);
            var method = type.GetMethod(nameof(TestMethod));

            var signature = method.GetSignature(false);

            Assert.AreEqual("public string TestMethod(string firstParam)", signature);
        }

        [TestMethod]
        public void Example_Signature_Invokable() {
            var type = typeof(Examples);
            var method = type.GetMethod(nameof(TestMethod));

            var signature = method.GetSignature(true);

            Assert.AreEqual("TestMethod(firstParam)", signature);
        }

        [TestMethod]
        public void Example_Signature_Generics() {
            var type  = typeof(Examples);
            var method = type.GetMethod(nameof(TestMethod2));

            var signature = method.GetSignature(false);

            Assert.AreEqual("public System.Collections.Generic.List<string> TestMethod2<T>(string firstParam, T secondParam)", signature);
        }

        [TestMethod]
        public void Example_Signature_Generics_Invokable() {
            var type  = typeof(Examples);
            var method = type.GetMethod(nameof(TestMethod2));

            var signature = method.GetSignature(true);

            Assert.AreEqual("TestMethod2<T>(firstParam, secondParam)", signature);
        }
    }
}
````
