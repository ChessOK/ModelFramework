using System;
using System.Linq;

using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation.Compatibility;
using ChessOk.ModelFramework.Validation.Validators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests.Validation.Validators
{
    [TestClass]
    public class MinLengthValidatorTests : ValidationTest
    {
        [TestMethod]
        public void ShouldFailIfLengthIsLessThanMinLengthWithString()
        {
            ValidationContext.Ensure("asd").HasMinLength(4);
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual(String.Format(Resources.Strings.MinLengthValidatorMessage, 4), ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldFailWithUserMessageIsSpecifiedWithString()
        {
            ValidationContext.Ensure("asd").HasMinLength(4, "Hello {0}");
            Assert.AreEqual("Hello 4", ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldSucceedIfLengthIsCorrectWithString()
        {
            ValidationContext.Ensure("asdff").HasMinLength(5);
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldNotFailWithNullObject()
        {
            ValidationContext.Ensure((string)null).HasMinLength(3);
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailIfLengthIsLessThanMinLengthOfArray()
        {
            ValidationContext.Ensure(new[] { 2, 3 }).HasMinLength(3);
            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailWithUserMessageIfSpecifiedWithArray()
        {
            ValidationContext.Ensure(new[] { 2, 3 }).HasMinLength(3, "Hello");
            Assert.AreEqual("Hello", ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldSucceedIfLengthIsCorrectWithArray()
        {
            ValidationContext.Ensure(new[] { 2, 3, 4 }).HasMinLength(3);
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowExceptionIfObjectIsNotAStringOrArray()
        {
            var validator = new MinLengthValidator(ValidationContext);
            validator.Validate(3);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowExceptionIfLengthIsNegative()
        {
            ValidationContext.Ensure("Hello").HasMinLength(-1);
        }

        [TestMethod]
        public void AttributeReturnsCorrectValidator()
        {
            var attr = new MinLengthAttribute(3);
            attr.ValidationContext = ValidationContext;
            attr.ErrorMessage = "Hello";

            var validator = (MinLengthValidator)attr.GetValidator();

            Assert.AreEqual(3, validator.Length);
            Assert.AreEqual("Hello", validator.Message);
        }
    }
}
