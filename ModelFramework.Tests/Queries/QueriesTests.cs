using Autofac;

using ChessOk.ModelFramework.Queries;
using ChessOk.ModelFramework.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests.Queries
{
    [TestClass]
    public class QueriesTests : ModelContextTest
    {
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            builder.Register(x => new TestQuery());
        }

        [TestMethod]
        public void ShouldExecuteQuery()
        {
            var query = Model.Query<TestQuery>();
            Assert.IsTrue(query.Result);
            Assert.IsTrue(query.Executed);
        }

        [TestMethod]
        public void ShouldUseInitializatorIfGiven()
        {
            var query = Model.Query<TestQuery>(x => x.Initialized = true);
            Assert.IsTrue(query.Initialized);
        }

        private class TestQuery : Query<bool>
        {
            public bool Initialized { get; set; }
            public bool Executed { get; private set; }

            protected override bool Execute()
            {
                Executed = true;
                Assert.IsNotNull(Context);
                return true;
            }
        }
    }
}
