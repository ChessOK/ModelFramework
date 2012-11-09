using System;
using System.Collections.Generic;

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
            Bus.Send(new TestMessage());
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void HandleShouldValidateMessageAndThrowIfInvalid()
        {
            Bus.Send(new ValidatableMessage());
        }

        [TestMethod]
        public void TryHandleShouldIgnoreValidationExceptionAndReturnFalseInThisCase()
        {
            var handled = Bus.TrySend(CreateErrorProneCommand());

            Assert.AreEqual(false, handled);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TryHandleShouldNotIgnoreOtherExceptions()
        {
            Bus.TrySend(new InlineCommand(() => { throw new InvalidOperationException(); }));
        }

        private Command CreateErrorProneCommand()
        {
            return new InlineCommand(
                () =>
                    {
                        Bus.ValidationContext.AddError("Hello");
                        Bus.ValidationContext.ThrowExceptionIfInvalid();
                    });
        }

        public class ValidatableMessage : IApplicationBusMessage, IValidatable
        {
            public void Validate(IValidationContext context)
            {
                context.AddError(string.Empty, "Hello!");
            }

            public string MessageName
            {
                get
                {
                    return "Validatable";
                }
            }
        }

        public class TestMessage : IApplicationBusMessage 
        {
            public string MessageName
            {
                get
                {
                    return "TestMessage";
                }
            }
        }

        public class TestMessage2 : IApplicationBusMessage 
        {
            public string MessageName
            {
                get
                {
                    return "TestMessage2";
                }
            }
        }

        public class TestHandler : IApplicationBusMessageHandler
        {
            public void Handle(IApplicationBusMessage ev)
            {
            }

            public IEnumerable<string> MessageNames
            {
                get { yield return "TestMessage2"; }
            }
        }
    }
}
