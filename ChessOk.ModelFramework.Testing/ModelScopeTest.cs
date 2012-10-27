using Autofac;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Testing
{
    [TestClass]
    public abstract class ModelScopeTest : ContainerTest
    {
        protected ModelContext Model { get; private set; }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<CoreModule>();
            builder.Register(x => new ModelContext(x.Resolve<ILifetimeScope>()));
        }

        [TestInitialize]
        public void InitializeModelContext()
        {
            Model = Container.Resolve<ModelContext>();
        }

        [TestCleanup]
        public void CleanupModelContext()
        {
            Model.Dispose();
        }
    }
}