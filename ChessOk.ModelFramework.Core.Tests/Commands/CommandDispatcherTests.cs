using System;
using System.Collections.Generic;

using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Commands.Filters;
using ChessOk.ModelFramework.Commands.Internals;
using ChessOk.ModelFramework.Commands.Messages;
using ChessOk.ModelFramework.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ChessOk.ModelFramework.Tests.Commands
{
    [TestClass]
    public class CommandDispatcherTests
    {
        private CommandDispatcher _dispatcher;
        private Mock<IApplicationBus> _bus;

        [TestInitialize]
        public void Initialize()
        {
            _bus = new Mock<IApplicationBus>();
            _dispatcher = new CommandDispatcher(_bus.Object);
        }

        [TestMethod]
        public void HandleShouldHandleCommands()
        {
            var command = new Mock<CommandBase>();
            var result = _dispatcher.Handle(command.Object);

            Assert.IsTrue(result);
            command.Verify(x => x.Invoke(), Times.Once());
        }

        [TestMethod]
        public void HandleShouldNotHandleOtherMessages()
        {
            var otherMessage = new Mock<IApplicationMessage>();
            var result = _dispatcher.Handle(otherMessage.Object);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HandleShouldInvokeAllCommandFilters()
        {
            var filtered = new FilteredCommand();
            _dispatcher.Handle(filtered);

            Assert.AreEqual(2, filtered.InvokedFilterNumbers.Count);
            Assert.IsTrue(filtered.Invoked);
        }

        [TestMethod]
        public void HandleShouldInvokeFiltersInTheirOrder()
        {
            var filtered = new FilteredCommand();
            _dispatcher.Handle(filtered);

            Assert.AreEqual(2, filtered.InvokedFilterNumbers[0]);
            Assert.AreEqual(1, filtered.InvokedFilterNumbers[1]);
        }

        [TestMethod]
        public void InvokeResultlessShouldInvokeTheCommand()
        {
            var command = new TestCommand();
            Assert.IsFalse(command.Executed);

            _dispatcher.Handle(command);
            Assert.IsTrue(command.Executed);
        }

        [TestMethod]
        public void InvokeResultfulShouldInvokeTheCommandAndSetResult()
        {
            var command = new TestResultfulCommand();
            Assert.IsFalse(command.Executed);
            Assert.IsFalse(command.Result);

            _dispatcher.Handle(command);

            Assert.IsTrue(command.Executed);
            Assert.IsTrue(command.Result);
        }

        [TestMethod]
        public void InvokeShouldRaiseInvokingEventBeforeCommandInvocation()
        {
            var command = new TestCommand();
            _bus.Setup(x => x.Handle(
                It.IsAny<ICommandInvokingMessage<TestCommand>>()))
                .Callback(() => Assert.IsFalse(command.Executed));

            _bus.Setup(x => x.Handle(It.IsAny<ICommandInvokingMessage<TestCommand>>()))
                .Callback<IApplicationMessage>(
                    x => Assert.AreSame(((ICommandInvokingMessage<TestCommand>)x).Command, command));

            _dispatcher.Handle(command);

            _bus.Verify(x => x.Handle(
                It.IsAny<ICommandInvokingMessage<TestCommand>>()), Times.Once());
        }

        [TestMethod]
        public void InvokeShouldRaiseInvokedEventAfterCommandInvocation()
        {
            var command = new TestCommand();
            _bus.Setup(x => x.Handle(
                It.IsAny<ICommandInvokedMessage<TestCommand>>()))
                .Callback(() => Assert.IsTrue(command.Executed));

            _bus.Setup(x => x.Handle(It.IsAny<ICommandInvokedMessage<TestCommand>>()))
                .Callback<IApplicationMessage>(
                    x => Assert.AreSame(((ICommandInvokedMessage<TestCommand>)x).Command, command));

            _dispatcher.Handle(command);

            _bus.Verify(x => x.Handle(
                It.IsAny<ICommandInvokedMessage<TestCommand>>()), Times.Once());
        }

        [TestMethod]
        public void ItShouldBePossibleToCancelCommandInvocationThroughInvokingEvent()
        {
            var command = new TestCommand();
            _bus.Setup(x => x.Handle(
                It.IsAny<ICommandInvokingMessage<TestCommand>>()))
                .Callback<IApplicationMessage>(x => ((ICommandInvokingMessage<TestCommand>)x).CancelInvocation());

            _dispatcher.Handle(command);

            Assert.IsFalse(command.Executed);
            _bus.Verify(x => x.Handle(It.IsAny<ICommandInvokedMessage<TestCommand>>()), Times.Never());
        }

        public class TestCommand : Command
        {
            public bool Executed { get; private set; }

            protected override void Execute()
            {
                Executed = true;
            }
        }

        public class TestResultfulCommand : Command<bool>
        {
            public bool Executed { get; private set; }

            protected override bool Execute()
            {
                Executed = true;
                return true;
            }
        }

        public class SomeFilterAttribute : CommandFilterAttribute
        {
            private readonly int _number;

            public SomeFilterAttribute(int number)
            {
                _number = number;
            }

            public override void OnInvoke(CommandFilterContext filterContext, Action commandAction)
            {
                var filteredCommand = (FilteredCommand)filterContext.Command;
                filteredCommand.InvokedFilterNumbers.Add(_number);

                commandAction();
            }
        }

        [SomeFilter(1, Order = 20), SomeFilter(2, Order = 0)]
        public class FilteredCommand : Command
        {
            public IList<int> InvokedFilterNumbers = new List<int>();

            public bool Invoked;

            protected override void Execute()
            {
                Invoked = true;
            }
        }
    }
}
