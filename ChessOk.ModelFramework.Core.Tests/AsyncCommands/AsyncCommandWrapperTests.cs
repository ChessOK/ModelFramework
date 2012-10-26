using System.ComponentModel.DataAnnotations;

using ChessOk.ModelFramework.AsyncCommands;
using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Validation;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests.AsyncCommands
{
    [TestClass]
    public class AsyncCommandWrapperTests : ValidationTest
    {
        [TestMethod]
        public void CommandPropertyShouldBeValidatedObject()
        {
            var command = new TestCommand();
            var wrapper = new AsyncCommand(command);

            ValidationContext.Ensure(wrapper).IsValid();

            Assert.IsFalse(ValidationContext.IsValid);
        }

        public class TestCommand : Command
        {
            [Required]
            public string Hello { get; set; }

            protected override void Execute()
            {
            }
        }
    }
}
