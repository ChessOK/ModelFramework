using System;
using System.Linq;

using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class AssertionEngineTests : ValidationTest
    {
        [TestMethod]
        public void IsValidShouldSetValidatorPropertiesAndCallValidate()
        {
            var validatorMock = new Mock<IValidator>();
            var obj = new object();
            ValidationContext.AssertObject(obj)
                .IsValid(validatorMock.Object);

            validatorMock.VerifySet(x => x.ValidationContext = It.Is<IValidationContext>(y => y != null));
            validatorMock.Verify(x => x.Validate(obj));
        }

        [TestMethod]
        public void CanValidatePropertyUsingStronglyTypedSyntax()
        {
            var validator = new Mock<IValidator>();
            var obj = new SomeClass();

            ValidationContext.AssertObject(obj)
                .AssertProperty(x => x.Name, x => x.IsValid(validator.Object));

            validator.VerifySet(x => x.ValidationContext = It.IsAny<IValidationContext>());
            validator.Verify(x => x.Validate(obj.Name));
        }

        [TestMethod]
        public void CanValidatePropertyUsingItsName()
        {
            var validator = new Mock<IValidator>();
            var obj = new SomeClass();

            ValidationContext.AssertObject(obj)
                .AssertProperty<string>("Name", x => x.IsValid(validator.Object));

            validator.VerifySet(x => x.ValidationContext = It.IsAny<IValidationContext>());
            validator.Verify(x => x.Validate(obj.Name));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowExceptionIfPropertyNotFound()
        {
            ValidationContext.AssertObject(new SomeClass()).AssertProperty<string>("Hello", x => { });
        }

        [TestMethod]
        public void PropertyAssertionShouldSilentlyExitIfObjectIsNull()
        {
            ValidationContext.AssertObject((string)null)
                .AssertProperty(x => x.Length, x => { })
                .AssertProperty<int>("Length", x => { });
        }

        [TestMethod]
        public void PropertyShouldPrependErrorKeysWithPropertyNames()
        {
            ValidationContext.AssertObject(new SomeClass())
                .AssertProperty(x => x.Name, x => x.IsPresent());

            Assert.AreEqual("Name", ValidationContext.Keys.Single());
        }

        private class SomeClass
        {
            public string Name { get; set; }
        }
    }
}
