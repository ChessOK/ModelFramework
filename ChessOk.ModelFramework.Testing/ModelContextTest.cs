using Autofac;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Testing
{
    [TestClass]
    public abstract class ModelContextTest : ContainerTest
    {
        protected ModelContext ModelContext { get; private set; }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<CoreModule>();
            builder.Register(x => new ModelContext(x.Resolve<ILifetimeScope>()));
        }

        [TestInitialize]
        public void InitializeModelContext()
        {
            ModelContext = Container.Resolve<ModelContext>();
        }

        [TestCleanup]
        public void CleanupModelContext()
        {
            ModelContext.Dispose();
        }
    }
}