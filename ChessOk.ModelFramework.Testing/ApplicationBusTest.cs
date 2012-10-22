using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Testing
{
    [TestClass]
    public abstract class ApplicationBusTest : ModelContextTest
    {
        protected IApplicationBus Bus { get; private set; }

        [TestInitialize]
        public void InitializeBus()
        {
            Bus = ModelContext.Get<IApplicationBus>();
        }
    }
}