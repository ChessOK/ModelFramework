﻿using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ChessOk.ModelFramework.Tests.AppBus
{
    [TestClass]
    public class ApplicationBusExtensionsTests
    {
        private Mock<IApplicationBus> _busMock;
        private Mock<IModelContext> _modelMock;

        [TestInitialize]
        public void Initialize()
        {
            _modelMock = new Mock<IModelContext>();
            _busMock = new Mock<IApplicationBus>();
            _busMock.SetupGet(x => x.Context).Returns(_modelMock.Object);
        }

        [TestMethod]
        public void InvokeRaisesCorrespondingMessage()
        {
            _modelMock.Setup(x => x.Get<Command>())
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
            _modelMock.Setup(x => x.Get<Command>()).Returns(commandMock.Object);

            _busMock.Object.Send<Command>(x =>
            {
                Assert.AreSame(commandMock.Object, x);
                _busMock.Verify(y => y.Send(It.IsAny<Command>()), Times.Never());
            });

            _busMock.Verify(x => x.Send(It.IsAny<Command>()), Times.Once());
        }
    }
}
