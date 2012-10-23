using ChessOk.ModelFramework.AsyncCommands;
using ChessOk.ModelFramework.AsyncCommands.Internals;
using ChessOk.ModelFramework.AsyncCommands.Messages;
using ChessOk.ModelFramework.AsyncCommands.Queues;
using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Commands.Internals;
using ChessOk.ModelFramework.Commands.Messages;
using ChessOk.ModelFramework.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ChessOk.ModelFramework.Tests.AsyncCommands
{
    [TestClass]
    public class AsyncCommandDispatcherTests
    {
        private AsyncCommandDispatcher _dispatcher;
        private Mock<IApplicationBus> _bus;
        private Mock<IAsyncCommandQueue> _queueMock;

        [TestInitialize]
        public void Initialize()
        {
            _bus = new Mock<IApplicationBus>();
            _dispatcher = new AsyncCommandDispatcher(_bus.Object);
            _queueMock = new Mock<IAsyncCommandQueue>();
            _bus.Setup(x => x.Context.Get<IAsyncCommandQueue>()).Returns(_queueMock.Object);
        }

        [TestMethod]
        public void HandleShouldEnqueueCommands()
        {
            var command = new AsyncCommand(new Mock<CommandBase>().Object);
            _dispatcher.Handle(command);

            _queueMock.Verify(x => x.Enqueue(It.IsAny<CommandBase>()));
        }

        [TestMethod]
        public void ItShouldRaiseSendingEventBeforeCommandSending()
        {
            var command = new TestCommand();
            _bus.Setup(x => x.Send(
                It.IsAny<IAsyncCommandSendingMessage<TestCommand>>()))
                .Callback(() => Assert.IsFalse(command.Executed));

            _bus.Setup(x => x.Send(It.IsAny<IAsyncCommandSendingMessage<TestCommand>>()))
                .Callback<IApplicationBusMessage>(
                    x => Assert.AreSame(((IAsyncCommandSendingMessage<TestCommand>)x).Command, command));

            _dispatcher.Handle(new AsyncCommand(command));

            _bus.Verify(x => x.Send(
                It.IsAny<IAsyncCommandSendingMessage<TestCommand>>()), Times.Once());
        }

        [TestMethod]
        public void ItShouldRaiseSentEventAfterCommandSending()
        {
            var command = new TestCommand();
            _bus.Setup(x => x.Send(
                It.IsAny<IAsyncCommandSentMessage<TestCommand>>()))
                .Callback(() => Assert.IsTrue(command.Executed));

            _bus.Setup(x => x.Send(It.IsAny<IAsyncCommandSentMessage<TestCommand>>()))
                .Callback<IApplicationBusMessage>(
                    x => Assert.AreSame(((IAsyncCommandSentMessage<TestCommand>)x).Command, command));

            _dispatcher.Handle(new AsyncCommand(command));

            _bus.Verify(x => x.Send(
                It.IsAny<IAsyncCommandSentMessage<TestCommand>>()), Times.Once());
        }

        [TestMethod]
        public void ItShouldBePossibleToCancelCommandSendingThroughSendingEvent()
        {
            var command = new TestCommand();
            _bus.Setup(x => x.Send(
                It.IsAny<IAsyncCommandSendingMessage<TestCommand>>()))
                .Callback<IApplicationBusMessage>(x => ((IAsyncCommandSendingMessage<TestCommand>)x).CancelSending());

            _dispatcher.Handle(new AsyncCommand(command));

            _queueMock.Verify(x => x.Enqueue(It.IsAny<CommandBase>()), Times.Never());
            _bus.Verify(x => x.Send(It.IsAny<IAsyncCommandSentMessage<TestCommand>>()), Times.Never());
        }

        public class TestCommand : Command
        {
            public bool Executed { get; private set; }

            protected override void Execute()
            {
                Executed = true;
            }
        }
    }
}
