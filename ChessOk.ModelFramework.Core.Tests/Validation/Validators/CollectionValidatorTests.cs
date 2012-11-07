using System;
using System.Collections.Generic;
using System.Linq;

using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Validation.Validators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class CollectionValidatorTests : ValidationTest
    {
        [TestMethod]
        public void ShouldValidateEveryObjectInCollection()
        {
            ValidationContext
                .Ensure(new List<SomeClass> { new SomeClass(), new SomeClass() })
                .HasValidItems();

            Assert.AreEqual(2, ValidationContext.Keys.Count);
        }

        [TestMethod]
        public void ShouldNotFailWithNullObject()
        {
            ValidationContext
                .Ensure((IList<object>)null)
                .HasValidItems();

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldNotFailWithEmptyCollection()
        {
            ValidationContext.Ensure(new List<object>()).HasValidItems();
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowExceptionIfObjectIsNotACollection()
        {
            var validator = new CollectionValidator(ValidationContext);
            validator.Validate(45);
        }

        [TestMethod]
        public void ShouldPrependElementIndexForEmptyErrorKeys()
        {
            ValidationContext
                .Ensure(new List<SomeClass> { new SomeClass() })
                .HasValidItems();

            Assert.AreEqual("[0]", ValidationContext.Keys.Single());
        }

        [TestMethod]
        public void ShouldPrependElementIndexWithDorForNonEmptyErrorKeys()
        {
            ValidationContext
                .Ensure(new [] { new SomeClass { KeyName = "Hello" } })
                .HasValidItems();

            Assert.AreEqual("[0].Hello", ValidationContext.Keys.Single());
        }

        [TestMethod]
        public void ObjectValidatorShouldNotAddDotIfKeyNameStartsWithObjectIndex()
        {
            ValidationContext
                .Ensure(new AnotherClass())
                .IsValid();

            Assert.AreEqual("Hello[0]", ValidationContext.Keys.Single());
        }

        public class AnotherClass
        {
            public AnotherClass()
            {
                Hello = new[] { new SomeClass() };
            }

            [ValidItems]
            public SomeClass[] Hello { get; set; }
        }

        public class SomeClass : IValidatable
        {
            public string KeyName = "";

            public void Validate(IValidationContext context)
            {
                context.AddError(KeyName, "asdsd");
            }
        }
    }
}
