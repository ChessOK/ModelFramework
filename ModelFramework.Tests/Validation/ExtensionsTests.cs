using System.Linq;

using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests.Validation
{
    [TestClass]
    public class ExtensionsTests : ValidationTest
    {
        [TestMethod]
        public void IsNotValidShouldAlwaysFail()
        {
            ValidationContext.Ensure(5).IsNotValid("foo");
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("foo", ValidationContext.GetErrors("").First());
        }

        [TestMethod]
        public void IsNotValidIfShouldSucceedIfConditionIsFailed()
        {
            ValidationContext.Ensure(4).IsNotValidIf(false, "foo");
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void IsNotValidIfShouldFailIfConditionSucceeded()
        {
            ValidationContext.Ensure(4).IsNotValidIf(true, "foo");
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("foo", ValidationContext.GetErrors("").First());
        }

        [TestMethod]
        public void IsNotValidIfNullShouldSucceedIfValueIsNotNull()
        {
            ValidationContext.Ensure(4).IsNotValidIfNull(4, "foo");
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void IsNotValidIfNullShouldFailIfValueIsNull()
        {
            ValidationContext.Ensure(4).IsNotValidIfNull(null, "foo");
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("foo", ValidationContext.GetErrors("").First());
        }

        [TestMethod]
        public void IsValidIfShouldSuccessIfConditionSucceeded()
        {
            ValidationContext.Ensure(4).IsValidIf(true, "foo");
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void IsValidIfShouldFailIfConditionFailed()
        {
            ValidationContext.Ensure(4).IsValidIf(false, "foo");
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("foo", ValidationContext.GetErrors("").First());
        }

        [TestMethod]
        public void IsGreaterThanShouldSucceedWithCorrectValue()
        {
            ValidationContext.Ensure(4).IsGreaterThan(3, "foo");
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void IsGreaterThanShouldFailWithIncorrectValue()
        {
            ValidationContext.Ensure(4).IsGreaterThan(5, "foo");
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("foo", ValidationContext.GetErrors("").First());
        }

        [TestMethod]
        public void IsGreaterThanShouldSucceedIfOtherIsNull()
        {
            ValidationContext.Ensure(3).IsGreaterThan(null, "foo");
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void IsGreaterThanOrEqualShouldSucceedWithCorrectValue()
        {
            ValidationContext.Ensure(3).IsGreaterThanOrEqual(3, "foo");
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void IsGreaterThanOrEqualShouldFailWithIncorrectValue()
        {
            ValidationContext.Ensure(4).IsGreaterThanOrEqual(5, "foo");
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("foo", ValidationContext.GetErrors("").First());
        }

        [TestMethod]
        public void IsGreaterThanOrEqualShouldSucceedIfOtherIsNull()
        {
            ValidationContext.Ensure(3).IsGreaterThanOrEqual(null, "foo");
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void IsLessThanShouldSucceedWithCorrectValue()
        {
            ValidationContext.Ensure(4).IsLessThan(5, "foo");
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void IsLessThanShouldFailWithIncorrectValue()
        {
            ValidationContext.Ensure(4).IsLessThan(3, "foo");
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("foo", ValidationContext.GetErrors("").First());
        }

        [TestMethod]
        public void IsLessThanShouldSucceedIfOtherIsNull()
        {
            ValidationContext.Ensure(3).IsLessThan(null, "foo");
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void IsLessThanOrEqualShouldSucceedWithCorrectValue()
        {
            ValidationContext.Ensure(3).IsLessThanOrEqual(3, "foo");
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void IsLessThanOrEqualShouldFailWithIncorrectValue()
        {
            ValidationContext.Ensure(4).IsLessThanOrEqual(3, "foo");
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual("foo", ValidationContext.GetErrors("").First());
        }

        [TestMethod]
        public void IsLessThanOrEqualShouldSucceedIfOtherIsNull()
        {
            ValidationContext.Ensure(3).IsLessThanOrEqual(null, "foo");
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void NotEqualsAttributeShouldSucceedIfStringRepresentationsAreDifferent()
        {
            var equals = new NotEqualsAttribute(2);

            var validator = equals.GetValidator(Container);
            validator.Validate(ValidationContext, "3");

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void NotEqualsAttributeShouldFailIfStringRepresentationsAreSame()
        {
            var equals = new NotEqualsAttribute(2);

            var validator = equals.GetValidator(Container);
            validator.Validate(ValidationContext, "2");

            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void NotEqualsAttributeShouldWorkFineWithNulls()
        {
            var equals = new NotEqualsAttribute(null);

            var validator = equals.GetValidator(Container);
            validator.Validate(ValidationContext, null);

            Assert.IsFalse(ValidationContext.IsValid);
        }
    }
}
