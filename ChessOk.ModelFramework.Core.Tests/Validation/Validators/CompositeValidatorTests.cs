using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Validation.Validators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class CompositeValidatorTests : ValidationTest
    {
        [TestMethod]
        public void ShouldRunAllValidators()
        {
            var validator = new CompositeValidator(
                new MaximumLengthValidator(3),
                new MaximumLengthValidator(1));
            ValidationContext.AssertObject("asdasd").IsValid(validator);

            Assert.AreEqual(2, ValidationContext[""].Count);
        }
    }
}
