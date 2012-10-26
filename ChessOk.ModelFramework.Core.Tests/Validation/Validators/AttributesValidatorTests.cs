using System.ComponentModel.DataAnnotations;
using System.Linq;

using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Validation.Internals;
using ChessOk.ModelFramework.Validation.Validators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class AttributesValidatorTests : ValidationTest
    {
        [TestMethod]
        public void ShouldNotFailIfObjectDoNotContainsAnyAttribute()
        {
            ValidationContext
                .Ensure(new AttributelessClass())
                .IsValid(new AttributesValidator(ValidationContext));

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldRunValidatorsWithinMainContextAndReplaceErrorKeys()
        {
            ValidationContext.Ensure(new AttributeClass())
                .IsValid(new AttributesValidator(ValidationContext));

            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("Hello", ValidationContext.Keys.First());
        }

        [TestMethod]
        public void AttributeShouldReturnCorrectValidator()
        {
            var attr = new ValidateAttributesAttribute();
            attr.ValidationContext = ValidationContext;
            Assert.IsInstanceOfType(attr.GetValidator(), typeof(AttributesValidator));
        }

        [TestMethod]
        public void ShouldNotPrependErrorKeysIfSpecified()
        {
            ValidationContext.Ensure(new AttributeClass()).IsValid(
                new AttributesValidator(ValidationContext) { DoNotModifyErrorKeys = true });

            Assert.AreEqual("", ValidationContext.Keys.First());
        }

        [TestMethod]
        public void ShouldValidateAnnotationsAttributes()
        {
            var annotations = new Annotations { Foo = "as" };
            var validator = new AttributesValidator(ValidationContext);
            validator.Validate(annotations);
            
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual(2, ValidationContext.Keys.Count);
        }

        private class AttributelessClass
        {
            public string Hello { get; set; }
        }

        private class AttributeClass
        {
            [Required]
            public string Hello { get; set; }
        }

        private class Annotations
        {
            [StringLength(5, MinimumLength = 3)]
            public string Foo { get; set; }

            [Range(3, 5)]
            public int Bar { get; set; }
        }
    }
}
