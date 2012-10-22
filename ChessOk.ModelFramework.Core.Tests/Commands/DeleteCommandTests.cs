using Autofac;

using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ChessOk.ModelFramework.Tests.Commands
{
    [TestClass]
    public class DeleteCommandTests : ApplicationBusTest
    {
        private Mock<IRepository<Entity>> _repositoryMock;

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            _repositoryMock = new Mock<IRepository<Entity>>();
            builder.Register(x => _repositoryMock.Object).As<IRepository<Entity>>();
        }

        [TestMethod]
        public void InvokeShouldCallDeleteMethodOnCorrespondingRepository()
        {
            var command = new DeleteCommand<Entity> { Entity = new TestEntity(), };

            Bus.Invoke(command);

            _repositoryMock.Verify(x => x.Delete(It.IsAny<Entity>()));
        }

        public class TestEntity : Entity { }
    }
}
