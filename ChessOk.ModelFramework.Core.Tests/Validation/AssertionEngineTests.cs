﻿using System;
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
            ValidationContext.Ensure(obj)
                .IsValid(validatorMock.Object);

            validatorMock.Verify(x => x.Validate(obj));
        }

        [TestMethod]
        public void CanValidatePropertyUsingStronglyTypedSyntax()
        {
            var validator = new Mock<IValidator>();
            var obj = new SomeClass();

            ValidationContext.Ensure(obj)
                .Property(x => x.Name, x => x.IsValid(validator.Object));

            validator.Verify(x => x.Validate(obj.Name));
        }

        [TestMethod]
        public void CanValidatePropertyUsingItsName()
        {
            var validator = new Mock<IValidator>();
            var obj = new SomeClass();

            ValidationContext.Ensure(obj)
                .Property<string>("Name", x => x.IsValid(validator.Object));

            validator.Verify(x => x.Validate(obj.Name));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowExceptionIfPropertyNotFound()
        {
            ValidationContext.Ensure(new SomeClass()).Property<string>("Hello", x => { });
        }

        [TestMethod]
        public void PropertyAssertionShouldSilentlyExitIfObjectIsNull()
        {
            ValidationContext.Ensure((string)null)
                .Property(x => x.Length, x => { })
                .Property<int>("Length", x => { });
        }

        [TestMethod]
        public void PropertyShouldPrependErrorKeysWithPropertyNames()
        {
            ValidationContext.Ensure(new SomeClass())
                .Property(x => x.Name, x => x.IsPresent());

            Assert.AreEqual("Name", ValidationContext.Keys.Single());
        }

        private class SomeClass
        {
            public string Name { get; set; }
        }
    }
}
