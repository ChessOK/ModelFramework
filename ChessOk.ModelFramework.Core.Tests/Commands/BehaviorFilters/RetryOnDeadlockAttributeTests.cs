using System;
using System.Collections.ObjectModel;
using System.Data;

using ChessOk.ModelFramework.Commands.Filters;
using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class RetryOnDeadlockAttributeTests
    {
        [TestMethod]
        public void ShouldInvokeOnceWhenNoExceptionsHasBeenThrown()
        {
            var attribute = new RetryOnDeadlockAttribute();
            int invokesCount = 0;
            Action action = () => invokesCount++;
            attribute.Apply(null, action);

            Assert.AreEqual(1, invokesCount);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldInvokeManyTimesIfDeadlockExceptionHasBeenEncountered()
        {
            SqlExceptionHelper.EmulateDeadlock = true;
            try
            {
                int invokesCount = 0;

                Action action = () =>
                {
                    invokesCount++;
                    throw new InvalidOperationException();
                };

                var attribute = new RetryOnDeadlockAttribute { RetryAttemptsCount = 3 };
                attribute.Apply(null, action);

                Assert.AreEqual(3, invokesCount);
            }
            catch (Exception)
            {
                SqlExceptionHelper.EmulateDeadlock = false;
                throw;
            }
        }

        [TestMethod]
        public void ShouldStopIfDeadlockHasBeenFixed()
        {
            SqlExceptionHelper.EmulateDeadlock = true;
            try
            {
                int invokesCount = 0;

                Action action = () =>
                {
                    invokesCount++;
                    if (invokesCount < 2)
                    {
                        throw new InvalidOperationException();
                    }
                };

                var attribute = new RetryOnDeadlockAttribute { RetryAttemptsCount = 3 };
                attribute.Apply(null, action);

                Assert.AreEqual(2, invokesCount);
            }
            catch (Exception)
            {
                SqlExceptionHelper.EmulateDeadlock = false;
                throw;
            }
        }
    }
}
