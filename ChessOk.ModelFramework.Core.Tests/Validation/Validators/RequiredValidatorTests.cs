using System;

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
            ValidationContext.AssertObject(new object())
                .IsPresent();

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailWithNullReferenceTypeInstances()
        {
            ValidationContext.AssertObject((object)null)
                .IsPresent();

            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailForEmptyStringsWithFalseAllowEmptyStringsOption()
        {
            ValidationContext.AssertObject(string.Empty)
                .IsPresent();

            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldSucceedForEmptyStringsWithAllowEmptyStringsOptionsSet()
        {
            ValidationContext.AssertObject(string.Empty)
                .IsPresent(allowEmptyStrings: true);

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailForWhitespacesStringsWithFalseAllowEmptyStringsOption()
        {
            ValidationContext.AssertObject("   ")
                .IsPresent();

            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldSucceedForWhitespacesStringsWithAllowEmptyStringsOptionsSet()
        {
            ValidationContext.AssertObject("  ")
                .IsPresent(allowEmptyStrings: true);

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void AttributeShouldReturnCorrectValidator()
        {
            var attr = new ValidatePresenceAttribute();
            Assert.IsInstanceOfType(attr.GetValidator(), typeof(PresenceValidator));
        }

        [TestMethod]
        public void ShouldNotCheckValueTypesForDefaultValues()
        {
            ValidationContext.AssertObject(default(bool)).IsValid(new PresenceValidator());
            ValidationContext.AssertObject(default(int)).IsValid(new PresenceValidator());
            ValidationContext.AssertObject(default(float)).IsValid(new PresenceValidator());
            ValidationContext.AssertObject(default(DateTime)).IsValid(new PresenceValidator());

            Assert.IsTrue(ValidationContext.IsValid);
        }
    }
}
