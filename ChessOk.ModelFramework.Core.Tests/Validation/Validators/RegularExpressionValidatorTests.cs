using System.Linq;

using ChessOk.ModelFramework.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests.Validation.Validators
{
    [TestClass]
    public class RegularExpressionValidatorTests : ValidationTest
    {
        [TestMethod]
        public void ShouldSucceedIfPatternHasBeenMatched()
        {
            ValidationContext.Ensure("Hello").Matches(@"Hello");
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailIfPatternHasNotBeenMatched()
        {
            ValidationContext.Ensure("World").Matches("Hello");
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual(
                string.Format(Resources.Strings.RegularExpressionMessage, "Hello"), ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldReturnUserMessageIfGiven()
        {
            ValidationContext.Ensure("foo").Matches("bar", "error {0}");
            Assert.AreEqual("error bar", ValidationContext.GetErrors("").First());
        }

        [TestMethod]
        public void ShouldSucceedOnNullObjects()
        {
            ValidationContext.Ensure((string)null).Matches("Hello");
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailIfOnlyPartOfAStringMatched()
        {
            ValidationContext.Ensure("Hello").Matches("[H]");
            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailIfStartIndexOfMatchIsGreaterThanZero()
        {
            ValidationContext.Ensure("Hello").Matches("[e]");
            Assert.IsFalse(ValidationContext.IsValid);
        }
    }
}
