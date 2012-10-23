using System;
using System.Collections.Generic;

using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ChessOk.ModelFramework.Tests.AppBus
{
    [TestClass]
    public class ApplicationEventHandlerTests : ApplicationBusTest
    {
        private TestHandler _handler;

        protected override void ConfigureContainer(Autofac.ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            _handler = new TestHandler();
            builder.RegisterEventHandler(x => _handler);
        }

        [TestMethod]
        public void ShouldHandleCorrespondingMessageType()
        {
            var message = new Mock<ITestMessage>();
            Bus.Send(message.Object);

            Assert.IsTrue(_handler.Handled);
            Assert.AreSame(message.Object, _handler.Message);
        }

        [TestMethod]
        public void ShouldHandleDerivedMessage()
        {
            var message = new Mock<IDerivedTestMessage>();
            Bus.Send(message.Object);

            Assert.IsTrue(_handler.Handled);
        }

        [TestMethod]
        public void ShouldNotHandleOtherMessages()
        {
            Bus.Send(new Mock<IApplicationBusMessage>().Object);
        }

        public interface IDerivedTestMessage : ITestMessage { }
        public interface ITestMessage : IApplicationBusMessage { }
        public class TestHandler : ApplicationBusMessageHandler<ITestMessage>
        {
            public bool Handled { get; private set; }
            public ITestMessage Message { get; private set; }

            protected override void Handle(ITestMessage message)
            {
                Handled = true;
                Message = message;
            }
        }
    }
}
