using System.Linq;

using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Validation.Validators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class RequiredValidatorTests : ValidationTest
    {
        [TestMethod]
        public void ShouldSucceedWithNonNullReferenceTypeInstances()
        {
            ValidationContext.Ensure(new object())
                .IsPresent();

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailWithNullReferenceTypeInstances()
        {
            ValidationContext.Ensure((object)null).IsPresent();

            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual(Resources.Strings.PresenceValidatorMessage, ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldFailWithUserStringIfSpecified()
        {
            ValidationContext.Ensure((object)null).IsPresent("foo");
            Assert.AreEqual("foo", ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldFailForEmptyStringsWithFalseAllowEmptyStringsOption()
        {
            ValidationContext.Ensure(string.Empty)
                .IsPresent();

            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldSucceedForEmptyStringsWithAllowEmptyStringsOptionsSet()
        {
            ValidationContext.Ensure(string.Empty)
                .IsPresent(allowEmptyStrings: true);

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailForWhitespacesStringsWithFalseAllowEmptyStringsOption()
        {
            ValidationContext.Ensure("   ")
                .IsPresent();

            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldSucceedForWhitespacesStringsWithAllowEmptyStringsOptionsSet()
        {
            ValidationContext.Ensure("  ")
                .IsPresent(allowEmptyStrings: true);

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldNotCheckValueTypesForDefaultValues()
        {
            ValidationContext.Ensure(default(bool)).IsValid(new RequiredValidator(ValidationContext));

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldWorkWithNullableTypes()
        {
            ValidationContext.Ensure((int?)null).IsPresent("foo");
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("foo", ValidationContext.GetErrors("").First());
        }
    }
}
