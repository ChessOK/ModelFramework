using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests.Commands
{
    [TestClass]
    public class InlineApplicationCommandTests : ApplicationBusTest
    {
        [TestMethod]
        public void ResultlessShouldRunSpecifiedActionWhenInvoking()
        {
            var invoked = false;
            var cmd = new InlineCommand(() => invoked = true);
            Assert.IsFalse(invoked);

            Bus.Handle(cmd);
            Assert.IsTrue(invoked);
        }

        [TestMethod]
        public void ResultfulShouldRunSpecifiedActionWhenInvoking()
        {
            var invoked = false;
            var cmd = new InlineCommand<bool>(() => invoked = true);
            Assert.IsFalse(invoked);

            Bus.Handle(cmd);
            Assert.IsTrue(invoked);
            Assert.IsTrue(cmd.Result);
        }
    }
}
