using System;
using System.Linq;

using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Validation.Compatibility;
using ChessOk.ModelFramework.Validation.Validators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class MaxLengthValidatorTests : ValidationTest
    {
        [TestMethod]
        public void ShouldFailWithDefaultMessageIfLengthExceedesMaxLengthWithString()
        {
            ValidationContext.Ensure("asd").HasMaxLength(2);
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual(String.Format(Resources.Strings.MaxLengthValidatorMessage, 2), ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldFailWithUserMessageIsSpecifiedWithString()
        {
            ValidationContext.Ensure("asd").HasMaxLength(2, "Hello {0}");
            Assert.AreEqual("Hello 2", ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldSucceedIfLengthIsCorrectWithString()
        {
            ValidationContext.Ensure("asdff").HasMaxLength(5);
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldNotFailWithNullObject()
        {
            ValidationContext.Ensure((string)null).HasMaxLength(3);
            Assert.IsTrue(ValidationContext.IsValid);
        }
        
        [TestMethod]
        public void ShouldFailIfLengthExceedesMaxLengthOfArray()
        {
            ValidationContext.Ensure(new []{ 2, 3 }).HasMaxLength(1);
            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailWithUserMessageIfSpecifiedWithArray()
        {
            ValidationContext.Ensure(new[] { 2, 3 }).HasMaxLength(1, "Hello");
            Assert.AreEqual("Hello", ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldSucceedIfLengthIsCorrectWithArray()
        {
            ValidationContext.Ensure(new[] { 2, 3, 4 }).HasMaxLength(3);
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowExceptionIfObjectIsNotAStringOrArray()
        {
            var validator = new MaxLengthValidator(ValidationContext);
            validator.Validate(3);
        }

        [TestMethod]
        public void ShouldIgnoreIfLengthIsEqualsToMinusOne()
        {
            // Для совместимости с RequiredAttribute
            ValidationContext.Ensure("Hello").HasMaxLength(-1);
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowExceptionIfLengthIsNegative()
        {
            ValidationContext.Ensure("Hello").HasMaxLength(-3);
        }

        [TestMethod]
        public void AttributeReturnsCorrectValidator()
        {
            var attr = new MaxLengthAttribute(3)
                {
                    ValidationContext = ValidationContext,
                    ErrorMessage = "Hello"
                };

            var validator = (MaxLengthValidator)attr.GetValidator();

            Assert.AreEqual(3, validator.Length);
            Assert.AreEqual("Hello", validator.Message);
        }
    }
}
