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
            var validator = new CompositeValidator(ValidationContext)
                {
                    Validators =
                        new[]
                            {
                                new MaximumLengthValidator(ValidationContext) { MaximumLength = 3 },
                                new MaximumLengthValidator(ValidationContext) { MaximumLength = 1 }
                            }
                };
            ValidationContext.Ensure("asdasd").IsValid(validator);

            Assert.AreEqual(2, ValidationContext[""].Count);
        }
    }
}
