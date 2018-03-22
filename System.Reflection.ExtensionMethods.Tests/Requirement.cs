using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace System.Reflection.ExtensionMethods.Tests
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
