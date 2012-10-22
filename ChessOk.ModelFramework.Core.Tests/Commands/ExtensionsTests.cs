using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Contexts;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ChessOk.ModelFramework.Tests.Commands
{
    [TestClass]
    public class ExtensionsTests
    {
        private Mock<IApplicationBus> _busMock;
        private Mock<IContext> _contextMock;

        [TestInitialize]
        public void Initialize()
        {
            _contextMock = new Mock<IContext>();
            _busMock = new Mock<IApplicationBus>();
            _busMock.SetupGet(x => x.Context).Returns(_contextMock.Object);
        }

        [TestMethod]
        public void InvokeSaveCommandShouldInvokeSaveCommand()
        {
            _contextMock.Setup(x => x.Get<SaveCommand<TestEntity>>())
                .Returns(new Mock<SaveCommand<TestEntity>>().Object);

            var entity = new TestEntity();
            _busMock.Object.InvokeSaveCommand(entity);
            _busMock.Verify(x => x.Handle(It.Is<SaveCommand<TestEntity>>(y => y.Entity == entity)));
        }

        [TestMethod]
        public void InvokeDeleteCommandShouldInvokeDeleteCommand()
        {
            _contextMock.Setup(x => x.Get<DeleteCommand<TestEntity>>())
                .Returns(new Mock<DeleteCommand<TestEntity>>().Object);

            var entity = new TestEntity();
            _busMock.Object.InvokeDeleteCommand(entity);
            _busMock.Verify(x => x.Handle(It.Is<DeleteCommand<TestEntity>>(y => y.Entity == entity)));
        }

        [TestMethod]
        public void InvokeRaisesCorrespondingMessage()
        {
            _contextMock.Setup(x => x.Get<Command>())
                .Returns(new Mock<Command>().Object);

            _busMock.Object.Invoke<Command>();
            _busMock.Verify(x => x.Handle(It.IsAny<Command>()));
        }

        [TestMethod]
        public void InvokeWithInstanceShouldRaiseCorrespondingMessage()
        {
            var command = new Mock<Command>();
            _busMock.Object.Invoke(command.Object);

            _busMock.Verify(x => x.Handle(It.IsAny<Command>()));
        }

        [TestMethod]
        public void InvokeWithInitializationShouldInitializeTheCommandAndRaiseMessage()
        {
            var commandMock = new Mock<Command>();
            _contextMock.Setup(x => x.Get<Command>()).Returns(commandMock.Object);

            _busMock.Object.Invoke<Command>(x =>
                {
                    Assert.AreSame(commandMock.Object, x);
                    _busMock.Verify(y => y.Handle(It.IsAny<Command>()), Times.Never());
                });

            _busMock.Verify(x => x.Handle(It.IsAny<Command>()), Times.Once());
        }

        public class TestEntity : Entity { }
    }
}
