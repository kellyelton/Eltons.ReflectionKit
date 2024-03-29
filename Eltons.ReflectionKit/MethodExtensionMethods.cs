﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System.Reflection;

namespace Eltons.ReflectionKit
{
    public static class MethodExtensionMethods
    {
        public static string GetSignature(this MethodInfo method, bool isInvokable)
        {
            return new MethodSignature().Build(method, isInvokable);
        }
    }
}