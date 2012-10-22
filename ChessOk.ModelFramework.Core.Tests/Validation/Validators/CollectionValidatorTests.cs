using System.Collections.Generic;
using System.Linq;

using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;

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
                .AssertObject(new List<SomeClass> { new SomeClass(), new SomeClass() })
                .HasValidItems();

            Assert.AreEqual(2, ValidationContext.Keys.Count);
        }

        [TestMethod]
        public void ShouldNotFailWithNullObject()
        {
            ValidationContext
                .AssertObject((IList<object>)null)
                .HasValidItems();

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldNotFailWithEmptyCollection()
        {
            ValidationContext.AssertObject(new List<object>()).HasValidItems();
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldPrependElementIndexForEmptyErrorKeys()
        {
            ValidationContext
                .AssertObject(new List<SomeClass> { new SomeClass() })
                .HasValidItems();

            Assert.AreEqual("[0]", ValidationContext.Keys.Single());
        }

        [TestMethod]
        public void ShouldPrependElementIndexWithDorForNonEmptyErrorKeys()
        {
            ValidationContext
                .AssertObject(new [] { new SomeClass { KeyName = "Hello" } })
                .HasValidItems();

            Assert.AreEqual("[0].Hello", ValidationContext.Keys.Single());
        }

        [TestMethod]
        public void ObjectValidatorShouldNotAddDotIfKeyNameStartsWithObjectIndex()
        {
            ValidationContext
                .AssertObject(new AnotherClass())
                .IsValid();

            Assert.AreEqual("Hello[0]", ValidationContext.Keys.Single());
        }

        private class AnotherClass
        {
            public AnotherClass()
            {
                Hello = new[] { new SomeClass() };
            }

            [ValidateCollectionItems]
            public SomeClass[] Hello { get; set; }
        }

        private class SomeClass : IValidatable
        {
            public string KeyName = "";

            public void Validate(IValidationContext context)
            {
                context.AddError(KeyName, "asdsd");
            }
        }
    }
}
