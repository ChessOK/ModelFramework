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
                .AssertObject(new AttributelessClass())
                .IsValid(new AttributesValidator());

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldRunValidatorsWithinMainContextAndReplaceErrorKeys()
        {
            ValidationContext.AssertObject(new AttributeClass())
                .IsValid(new AttributesValidator());

            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("Hello", ValidationContext.Keys.First());
        }

        [TestMethod]
        public void AttributeShouldReturnCorrectValidator()
        {
            var attr = new ValidateAttributesAttribute();
            Assert.IsInstanceOfType(attr.GetValidator(), typeof(AttributesValidator));
        }

        private class AttributelessClass
        {
            public string Hello { get; set; }
        }

        private class AttributeClass
        {
            [ValidatePresence]
            public string Hello { get; set; }
        }
    }
}
