using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Testing
{
    [TestClass]
    public abstract class ApplicationBusTest : ModelScopeTest
    {
        protected IApplicationBus Bus { get; private set; }

        [TestInitialize]
        public void InitializeBus()
        {
            Bus = Model.Get<IApplicationBus>();
        }

        [TestCleanup]
        public void CleanupBus()
        {
            Bus.Dispose();
        }
    }
}