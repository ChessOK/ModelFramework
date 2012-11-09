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
                .IsValid(new AttributesValidator());

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldRunValidatorsWithinMainContextAndReplaceErrorKeys()
        {
            ValidationContext.Ensure(new AttributeClass())
                .IsValid(new AttributesValidator());

            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("Hello", ValidationContext.Keys.First());
        }

        [TestMethod]
        public void AttributeShouldReturnCorrectValidator()
        {
            var attr = new ValidateAttributesAttribute();
            Assert.IsInstanceOfType(attr.GetValidator(Container), typeof(AttributesValidator));
        }

        [TestMethod]
        public void ShouldNotPrependErrorKeysIfSpecified()
        {
            ValidationContext.Ensure(new AttributeClass()).IsValid(
                new AttributesValidator());

            Assert.IsTrue(ValidationContext[""].Any());
        }

        [TestMethod]
        public void ShouldValidateAnnotationsAttributes()
        {
            var annotations = new Annotations { Foo = "as" };
            var validator = new AttributesValidator();
            validator.Validate(ValidationContext, annotations);
            
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual(2, ValidationContext.Keys.Count);
        }

        public class AttributelessClass
        {
            public string Hello { get; set; }
        }

        public class AttributeClass
        {
            [Required]
            public string Hello { get; set; }

            [Required, FlatErrorKeys]
            public string World { get; set; }
        }

        public class Annotations
        {
            [StringLength(5, MinimumLength = 3)]
            public string Foo { get; set; }

            [Range(3, 5)]
            public int Bar { get; set; }
        }
    }
}
