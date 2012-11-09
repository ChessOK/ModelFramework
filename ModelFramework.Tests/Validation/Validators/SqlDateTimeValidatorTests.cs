using System;
using System.Linq;

using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Validation.Validators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class SqlDateTimeValidatorTests : ValidationTest
    {
        [TestMethod]
        public void ShouldSucceedWithValidSqlDateTime()
        {
            ValidationContext.Ensure(new DateTime(2012, 10, 10)).IsSqlDateTime();
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldFailWithInvalidSqlDateTime()
        {
            ValidationContext.Ensure(DateTime.MinValue).IsSqlDateTime();
            ValidationContext.Ensure(DateTime.MaxValue).IsSqlDateTime();
            Assert.IsFalse(ValidationContext.IsValid);
            Assert.AreEqual(2, ValidationContext[""].Count);
            Assert.AreEqual(Resources.Strings.SqlDateTimeValidatorMessage, ValidationContext[""].First());
        }

        [TestMethod]
        public void ShouldFailWithUserMessageIfGiven()
        {
            ValidationContext.Ensure(DateTime.MinValue).IsSqlDateTime("Hello");
            Assert.AreEqual("Hello", ValidationContext[""].First());
        }

        [TestMethod]
        public void AttributeShouldReturnCorrectValidator()
        {
            var attr = new SqlDateTimeAttribute { ErrorMessage = "Hello" };

            var validator = (SqlDateTimeValidator)attr.GetValidator(Container);

            Assert.AreEqual("Hello", validator.Message);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowAnExceptionIfAppliedToTypeOtherThanDateTime()
        {
            var validator = new SqlDateTimeValidator();
            validator.Validate(ValidationContext, 34);
        }

        [TestMethod]
        public void ShouldIgnoreNullValues()
        {
            var validator = new SqlDateTimeValidator();
            validator.Validate(ValidationContext, null);

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldWorkWithNullableDateTime()
        {
            DateTime? value = DateTime.Now;
            var validator = new SqlDateTimeValidator();
            validator.Validate(ValidationContext, value);

            Assert.IsTrue(ValidationContext.IsValid);
        }
    }
}
