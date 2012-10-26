using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Contexts;
using ChessOk.ModelFramework.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ChessOk.ModelFramework.Tests.AppBus
{
    [TestClass]
    public class ApplicationBusExtensionsTests
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
        public void ShouldCreateMessagesThroughContainer()
        {
            var initialized = false;
            _busMock.Object.Create<IApplicationBusMessage>(x => initialized = true);

            Assert.IsTrue(initialized);
            _contextMock.Verify(x => x.Get<IApplicationBusMessage>());
        }

        [TestMethod]
        public void InvokeRaisesCorrespondingMessage()
        {
            _contextMock.Setup(x => x.Get<Command>())
                .Returns(new Mock<Command>().Object);

            _busMock.Object.Send<Command>();
            _busMock.Verify(x => x.Send(It.IsAny<Command>()));
        }

        [TestMethod]
        public void InvokeWithInstanceShouldRaiseCorrespondingMessage()
        {
            var command = new Mock<Command>();
            _busMock.Object.Send(command.Object);

            _busMock.Verify(x => x.Send(It.IsAny<Command>()));
        }

        [TestMethod]
        public void InvokeWithInitializationShouldInitializeTheCommandAndRaiseMessage()
        {
            var commandMock = new Mock<Command>();
            _contextMock.Setup(x => x.Get<Command>()).Returns(commandMock.Object);

            _busMock.Object.Send<Command>(x =>
            {
                Assert.AreSame(commandMock.Object, x);
                _busMock.Verify(y => y.Send(It.IsAny<Command>()), Times.Never());
            });

            _busMock.Verify(x => x.Send(It.IsAny<Command>()), Times.Once());
        }
    }
}
