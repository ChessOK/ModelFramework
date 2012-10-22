using System;

using Autofac;

using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests
{
    [TestClass]
    public class ApplicationBusTests : ApplicationBusTest
    {
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            builder.RegisterEventHandler(x => new TestHandler());
            builder.RegisterEventHandler(x => new TestHandler());
        }

        [TestMethod]
        public void ShouldNotFailIfThereAreNoHandlersForThisMessage()
        {
            Bus.Handle(new TestMessage());
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationErrorsException))]
        public void HandleShouldValidateMessageAndThrowIfInvalid()
        {
            Bus.Handle(new ValidatableMessage());
        }

        [TestMethod]
        public void TryHandleShouldIgnoreValidationExceptionAndReturnFalseInThisCase()
        {
            var handled = Bus.TryHandle(CreateErrorProneCommand());

            Assert.AreEqual(false, handled);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TryHandleShouldNotIgnoreOtherExceptions()
        {
            Bus.TryHandle(new InlineCommand(() => { throw new InvalidOperationException(); }));
        }

        private Command CreateErrorProneCommand()
        {
            return new InlineCommand(
                () =>
                    {
                        Bus.Validation.AddError("Hello");
                        Bus.Validation.ThrowExceptionIfInvalid();
                    });
        }

        public class ValidatableMessage : IApplicationMessage, IValidatable
        {
            public void Validate(IValidationContext context)
            {
                context.AddError(string.Empty, "Hello!");
            }
        }

        public class TestMessage : IApplicationMessage { }
        public class TestEvent2 : IApplicationMessage { }
        public class TestHandler : IApplicationEventHandler
        {
            public bool Handle(IApplicationMessage ev)
            {
                return ev is TestEvent2;
            }
        }
    }
}
