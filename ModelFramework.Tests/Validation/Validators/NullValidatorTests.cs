using ChessOk.ModelFramework.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class NullValidatorTests : ValidationTest
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
    }
}
