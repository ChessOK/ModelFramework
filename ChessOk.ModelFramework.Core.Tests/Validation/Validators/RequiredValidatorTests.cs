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
            ValidationContext.Ensure(new object())
                .IsPresent();

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailWithNullReferenceTypeInstances()
        {
            ValidationContext.Ensure((object)null)
                .IsPresent();

            Assert.IsFalse(ValidationContext.IsValid);
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
        public void AttributeShouldReturnCorrectValidator()
        {
            var attr = new ValidateRequiredAttribute();
            attr.ValidationContext = ValidationContext;
            Assert.IsInstanceOfType(attr.GetValidator(), typeof(RequiredValidator));
        }

        [TestMethod]
        public void ShouldNotCheckValueTypesForDefaultValues()
        {
            ValidationContext.Ensure(default(bool)).IsValid(new RequiredValidator(ValidationContext));
            ValidationContext.Ensure(default(int)).IsValid(new RequiredValidator(ValidationContext));
            ValidationContext.Ensure(default(float)).IsValid(new RequiredValidator(ValidationContext));
            ValidationContext.Ensure(default(DateTime)).IsValid(new RequiredValidator(ValidationContext));

            Assert.IsTrue(ValidationContext.IsValid);
        }
    }
}
