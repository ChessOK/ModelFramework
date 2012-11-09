using System.Linq;

using ChessOk.ModelFramework.Testing;
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
            ValidationContext.Ensure(true)
                .IsValid(new DelegateValidator
                    {
                        Delegate = x => (bool)x,
                        Message = "Some message"
                    });

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailIfDelegateReturnsFalse()
        {
            ValidationContext.Ensure(false)
                .IsValid(new DelegateValidator
                {
                    Delegate = x => (bool)x,
                    Message = "Some message"
                });

            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("Some message", ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldWorkWithPropertiesEither()
        {
            ValidationContext.Ensure("Hello")
                .ItsProperty(x => x.Length, x => x.IsTrue(y => y.Equals(3), "Hello ;)"));

            Assert.IsFalse(ValidationContext.IsValid);
        }
    }
}
