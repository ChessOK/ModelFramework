using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Commands.Internals;
using ChessOk.ModelFramework.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests.Commands
{
    [TestClass]
    public class ApplicationCommandTests : ApplicationBusTest
    {
        [TestMethod]
        public void BaseCommandShouldBeInitializedBeforeInvokeAndInvoked()
        {
            var command = new TestBaseCommand();
            Bus.Handle(command);
            Assert.IsTrue(command.Invoked);
        }

        [TestMethod]
        public void ResultlessCommandShouldBeExecuted()
        {
            var command = new TestResultlessCommand();
            Bus.Handle(command);
            Assert.IsTrue(command.Executed);
        }

        [TestMethod]
        public void ResultfulCommandShouldBeExecutedAndHaveResult()
        {
            var command = new TestResultfulCommand();
            Assert.AreEqual(default(bool),  command.Result);

            Bus.Handle(command);

            Assert.IsTrue(command.Executed);
            Assert.IsTrue(command.Result);
        }

        private class TestResultfulCommand : Command<bool>
        {
            public bool Executed { get; private set; }

            protected override bool Execute()
            {
                Executed = true;
                return true;
            }
        }

        private class TestResultlessCommand : Command
        {
            public bool Executed { get; private set; }

            protected override void Execute()
            {
                Executed = true;
            }
        }

        private class TestBaseCommand : CommandBase
        {
            public bool Invoked { get; private set; }

            public override void Invoke()
            {
                Invoked = true;
                Assert.IsNotNull(Bus);
                Assert.IsNotNull(Validation);
                Assert.IsNotNull(Context);
            }
        }
    }
}
