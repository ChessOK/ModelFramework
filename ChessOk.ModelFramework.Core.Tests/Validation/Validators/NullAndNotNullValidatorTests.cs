using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class NullAndNotNullValidatorTests : ValidationTest
    {
        [TestMethod]
        public void NullShouldSucceedOnNullValue()
        {
            ValidationContext.Ensure((string)null).IsNull();
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void NullShouldFailOnNonNullValue()
        {
            ValidationContext.Ensure(new object()).IsNull();
            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void NotNullShouldFailOnNullValue()
        {
            ValidationContext.Ensure((string)null).IsNotNull();
            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void NotNullShouldSucceedOnNonNullValue()
        {
            ValidationContext.Ensure(new object()).IsNotNull();
            Assert.IsTrue(ValidationContext.IsValid);
        }
    }
}
