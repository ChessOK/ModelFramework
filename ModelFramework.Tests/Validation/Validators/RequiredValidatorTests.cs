using System.Linq;

using ChessOk.ModelFramework.Testing;
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
                .IsRequired();

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailWithNullReferenceTypeInstances()
        {
            ValidationContext.Ensure((object)null).IsRequired();

            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual(Resources.Strings.PresenceValidatorMessage, ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldFailWithUserStringIfSpecified()
        {
            ValidationContext.Ensure((object)null).IsRequired("foo");
            Assert.AreEqual("foo", ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldFailForEmptyStringsWithFalseAllowEmptyStringsOption()
        {
            ValidationContext.Ensure(string.Empty)
                .IsRequired();

            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldSucceedForEmptyStringsWithAllowEmptyStringsOptionsSet()
        {
            ValidationContext.Ensure(string.Empty)
                .IsRequired(allowEmptyStrings: true);

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailForWhitespacesStringsWithFalseAllowEmptyStringsOption()
        {
            ValidationContext.Ensure("   ")
                .IsRequired();

            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldSucceedForWhitespacesStringsWithAllowEmptyStringsOptionsSet()
        {
            ValidationContext.Ensure("  ")
                .IsRequired(allowEmptyStrings: true);

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldNotCheckValueTypesForDefaultValues()
        {
            ValidationContext.Ensure(default(bool)).IsValid(new RequiredValidator());

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldWorkWithNullableTypes()
        {
            ValidationContext.Ensure((int?)null).IsRequired("foo");
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("foo", ValidationContext.GetErrors("").First());
        }
    }
}
