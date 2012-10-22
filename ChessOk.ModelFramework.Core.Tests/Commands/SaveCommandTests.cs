using Autofac;

using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ChessOk.ModelFramework.Tests.Commands
{
    [TestClass]
    public class SaveCommandTests : ApplicationBusTest
    {
        private Mock<IRepository<Entity>> _repositoryMock;

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            _repositoryMock = new Mock<IRepository<Entity>>();
            builder.Register(x => _repositoryMock.Object).As<IRepository<Entity>>();
        }

        [TestMethod]
        public void InvokeShouldCallSaveMethodOnCorrespondingRepository()
        {
            var command = new SaveCommand<Entity> { Entity = new TestEntity(), };

            Bus.Invoke(command);

            _repositoryMock.Verify(x => x.Save(It.IsAny<Entity>()));
        }

        public class TestEntity : Entity, IValidatable 
        {
            public bool ShouldCauseError { get; set; }

            public void Validate(IValidationContext context)
            {
                if (ShouldCauseError) { context.AddError("asdasd", "hello"); }
            }
        }
    }
}
