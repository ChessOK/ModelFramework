using System.Linq;

using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Validation.Validators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class DelegateValidatorTests : ValidationTest
    {
        [TestMethod]
        public void ShouldSucceedIfDelegateReturnsTrue()
        {
            ValidationContext.AssertObject(true)
                .IsValid(new DelegateValidator<bool>(x => x, "Some message"));

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailIfDelegateReturnsFalse()
        {
            ValidationContext.AssertObject(false)
                .IsValid(new DelegateValidator<bool>(x => x, "Some message"));

            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("Some message", ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldIgnoreObjectOfOtherTypes()
        {
            ValidationContext.AssertObject("Hello")
                .IsValid(new DelegateValidator<bool>(x => x, "Some"));

            Assert.IsTrue(ValidationContext.IsValid);
        }
    }
}
