using Autofac;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Testing
{
    [TestClass]
    public abstract class ContainerTest
    {
        protected IContainer Container { get; private set; }

        [TestInitialize]
        public void InitializeContainer()
        {
            var builder = new ContainerBuilder();
            
            ConfigureContainer(builder);
            Container = builder.Build();
        }

        [TestCleanup]
        public void CleanupContainer()
        {
            Container.Dispose();
        }

        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
        }
    }
}