using System;

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
        }

        [TestMethod]
        public void AttributeShouldReturnCorrectValidator()
        {
            var attr = new ValidateSqlDateTimeAttribute();
            attr.ValidationContext = ValidationContext;
            Assert.IsInstanceOfType(attr.GetValidator(), typeof(SqlDateTimeValidator));
        }
    }
}
