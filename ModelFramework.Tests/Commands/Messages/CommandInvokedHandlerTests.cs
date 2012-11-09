using Autofac;

using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Commands.Messages;
using ChessOk.ModelFramework.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ChessOk.ModelFramework.Tests.Commands.EventHandlers
{
    [TestClass]
    public class CommandInvokedHandlerTests : ApplicationBusTest
    {
        private TestHandler _handler;

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            _handler = new TestHandler();
            builder.RegisterMessageHandler(c => _handler);
        }

        [TestMethod]
        public void ShouldBeRunnedWhenHandlingCorrespondingMessage()
        {
            var commandMock = new Mock<CommandBase>();
            var messageMock = new Mock<ICommandInvokedMessage<CommandBase>>();
            messageMock.SetupGet(x => x.MessageName).Returns(CommandInvokedMessage<object>.GetMessageName());
            messageMock.SetupGet(x => x.Command).Returns(() => commandMock.Object);

            Bus.Send(messageMock.Object);

            Assert.IsTrue(_handler.Handled);
            Assert.AreSame(commandMock.Object, _handler.Command);
        }

        private class TestHandler : CommandInvokedHandler<CommandBase>
        {
            public bool Handled { get; private set; }
            public CommandBase Command { get; private set; }

            protected override void Handle(CommandBase command)
            {
                Handled = true;
                Command = command;
            }
        }
    }
}
