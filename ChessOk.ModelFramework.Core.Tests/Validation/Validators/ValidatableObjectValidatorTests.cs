using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Validation.Validators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class ValidatableObjectValidatorTests : ValidationTest
    {
        [TestMethod]
        public void ShouldWorkFineForObjectsThatNotImplementsInterface()
        {
            ValidationContext.AssertObject("Hello").IsValid(new ValidatableObjectValidator());
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldCallValidateMethod()
        {
            ValidationContext.AssertObject(new ValidatableClass()).IsValid(new ValidatableObjectValidator());
            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void AttributeShouldReturnCorrectValidator()
        {
            var attr = new ValidateObjectAttribute();
            Assert.IsInstanceOfType(attr.GetValidator(), typeof(CompositeValidator));
        }

        private class ValidatableClass : IValidatable
        {
            public void Validate(IValidationContext context)
            {
                context.AddError("asdasd", "Hello");
            }
        }
    }
}
