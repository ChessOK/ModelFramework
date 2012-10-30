using System.Linq;

using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class ValidationContextTests : ValidationTest
    {
        [TestMethod]
        public void EmptyContextShouldBeValid()
        {
            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ShouldNotBeValidIfThereAreErrors()
        {
            ValidationContext.AddError("SomeKey", "Some message");
            Assert.IsFalse(ValidationContext.IsValid);
        }

        [TestMethod]
        public void AddErrorWithEmptyKeyShouldBeValid()
        {
            ValidationContext.AddError(string.Empty, "Some message");
            Assert.IsTrue(ValidationContext[string.Empty].Contains("Some message"));
        }

        [TestMethod]
        public void AddErrorWithNullKeyShouldBeEqualToAddingWithEmptyKey()
        {
            ValidationContext.AddError(null, "Some message");
            Assert.IsTrue(ValidationContext[string.Empty].Contains("Some message"));
            Assert.IsTrue(ValidationContext[null].Contains("Some message"));
        }

        [TestMethod]
        public void GetErrorsShouldReturnEmptyCollectionIfKeyNotFound()
        {
            Assert.AreEqual(0, ValidationContext[""].Count);
        }

        [TestMethod]
        public void OneKeyCanContainMultipleErrors()
        {
            ValidationContext.AddError("key", "1");
            ValidationContext.AddError("key", "2");
            Assert.AreEqual(2, ValidationContext["key"].Count);
        }

        [TestMethod]
        public void ErrorKeysShouldBeCaseSensitive()
        {
            ValidationContext.AddError("Key", "1");
            ValidationContext.AddError("kEy", "2");
            Assert.AreEqual(2, ValidationContext.Keys.Count);
        }

        [TestMethod]
        public void KeysShouldReturnAllErrorKeys()
        {
            ValidationContext.AddError("key1", "1");
            ValidationContext.AddError("key2", "3");
            Assert.AreEqual(2, ValidationContext.Keys.Count);
        }

        [TestMethod]
        public void ItShouldBePossibleToRemoveErrors()
        {
            ValidationContext.AddError("key", "1");
            ValidationContext.AddError("key2", "2");
            ValidationContext.RemoveErrors("key");

            Assert.AreEqual(0, ValidationContext["key"].Count);
            Assert.AreEqual(1, ValidationContext.Keys.Count);
            Assert.AreEqual("key2", ValidationContext.Keys.First());
        }

        [TestMethod]
        public void ItShouldBePossibleToClearContext()
        {
            ValidationContext.AddError("key", "1");
            ValidationContext.Clear();

            Assert.IsTrue(ValidationContext.IsValid);
        }

        [TestMethod]
        public void ThrowExceptionIfInvalidShouldNotThrowIfItIsValid()
        {
            ValidationContext.ThrowExceptionIfInvalid();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ThrowExceptionIfInvalidShouldThrowIfItIsInvalid()
        {
            ValidationContext.AddError(null, "asd");
            ValidationContext.ThrowExceptionIfInvalid();
        }

        [TestMethod]
        public void ReplaceShouldReplaceWithRegexMatchedStringsOnAddError()
        {
            using (ValidationContext.PrefixErrorKeysWithName("User"))
            {
                ValidationContext.AddError("Name", "Not valid");
            }

            Assert.AreEqual(1, ValidationContext.Keys.Count);
            Assert.AreEqual("User.Name", ValidationContext.Keys.First());
        }

        [TestMethod]
        public void ReplaceShouldSupportNestedInvocations()
        {
            using (ValidationContext.PrefixErrorKeysWithName("Admin"))
            using (ValidationContext.PrefixErrorKeysWithName("Name"))
            {
                ValidationContext.AddError("Last", "Hello");
            }

            Assert.AreEqual(1, ValidationContext.Keys.Count);
            Assert.AreEqual("Admin.Name.Last", ValidationContext.Keys.First());
        }

        [TestMethod]
        public void ReplaceShouldSupportIndexedKeys()
        {
            using (ValidationContext.PrefixErrorKeysWithName("User"))
            {
                // Эмулируем механизм CollectionValidator
                ValidationContext.AddError("[3].Name", "Hello");
            }

            Assert.AreEqual("User[3].Name", ValidationContext.Keys.First());
        }
    }
}
