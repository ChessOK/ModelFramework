using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class StringLengthValidatorTests : ValidationTest
    {
        [TestMethod]
        public void ShouldFailIfLengthExceedesMaxLength()
        {
            ValidationContext.AssertObject("asd").HasMaximumLength(2);
            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldSucceedIfLengthIsCorrect()
        {
            ValidationContext.AssertObject("asdff").HasMaximumLength(5);
            Assert.IsTrue(ValidationContext.IsValid);
        }
    }
}
