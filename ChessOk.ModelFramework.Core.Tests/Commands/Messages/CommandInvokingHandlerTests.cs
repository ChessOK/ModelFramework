using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Commands.Internals;
using ChessOk.ModelFramework.Commands.Messages;
using ChessOk.ModelFramework.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ChessOk.ModelFramework.Tests.Commands.EventHandlers
{
    [TestClass]
    public class CommandInvokingHandlerTests : ApplicationBusTest
    {
        private TestHandler _handler;

        protected override void ConfigureContainer(Autofac.ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            _handler = new TestHandler();
            builder.RegisterEventHandler(c => _handler);
        }

        [TestMethod]
        public void ShouldBeRunnedWhenHandlingCorrespondingMessage()
        {
            var commandMock = new Mock<CommandBase>();
            var messageMock = new Mock<ICommandInvokingMessage<CommandBase>>();
            messageMock.SetupGet(x => x.Command).Returns(() => commandMock.Object);

            Bus.Send(messageMock.Object);
            
            Assert.IsTrue(_handler.Handled);
            Assert.AreSame(commandMock.Object, _handler.Command);
            messageMock.Verify(x => x.CancelInvocation(), Times.Never());
        }

        [TestMethod]
        public void ShouldBeRunnedWhenHandlingCorrespondingMessageWithDerivedTypeParameter()
        {
            var messageMock = new Mock<ICommandInvokingMessage<Command>>();
            Bus.Send(messageMock.Object);

            Assert.IsTrue(_handler.Handled);
        }

        [TestMethod]
        public void ShouldBePossibleToCancelInvocation()
        {
            var messageMock = new Mock<ICommandInvokingMessage<CommandBase>>();
            _handler.CancelInvocation = true;

            Bus.Send(messageMock.Object);

            messageMock.Verify(x => x.CancelInvocation(), Times.Once());
        }

        public class TestHandler : CommandInvokingHandler<CommandBase>
        {
            public bool Handled { get; private set; }
            public CommandBase Command { get; private set; }

            public bool CancelInvocation { get; set; }

            protected override void Handle(CommandBase command, out bool cancelInvocation)
            {
                Command = command;
                Handled = true;
                cancelInvocation = CancelInvocation;
            }
        }
    }
}
