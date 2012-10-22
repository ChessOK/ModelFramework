using ChessOk.ModelFramework.AsyncCommands;
using ChessOk.ModelFramework.Commands;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ChessOk.ModelFramework.Tests.AsyncCommands
{
    [TestClass]
    public class ExtensionsTests
    {
        private Mock<IApplicationBus> _appBusMock;

        [TestInitialize]
        public void Initialize()
        {
            _appBusMock = new Mock<IApplicationBus>();
            _appBusMock.Setup(x => x.Context.Get<TestCommand>()).Returns(new TestCommand());
        }

        [TestMethod]
        public void SendWithInstanceShouldCreateWrapperAndHandleIt()
        {
            var command = new TestCommand();
            _appBusMock.Object.Send(command);
            _appBusMock.Verify(x => x.Handle(It.Is<AsyncCommandWrapperMessage>(y => y.Command == command)));
        }

        [TestMethod]
        public void SendWithTypeOnlyShouldInstantinateCommandCreateWrapperAndHandleIt()
        {
            _appBusMock.Object.Send<TestCommand>();
            _appBusMock.Verify(x => x.Handle(It.Is<AsyncCommandWrapperMessage>(y => y.Command != null)));
        }

        [TestMethod]
        public void SendWithInitializationShouldCallInitializer()
        {
            var called = false;
            _appBusMock.Object.Send<TestCommand>(x => { called = true; Assert.IsNotNull(x); });
            Assert.IsTrue(called);
            _appBusMock.Verify(x => x.Handle(It.Is<AsyncCommandWrapperMessage>(y => y.Command != null)));
        }

        public class TestCommand : Command
        {
            protected override void Execute()
            {
            }
        }
    }
}
