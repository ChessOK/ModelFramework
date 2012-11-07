using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using ChessOk.ModelFramework.Commands;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Tests.AsyncCommands
{
    [TestClass]
    public class AsyncCommandsTests
    {
        [TestMethod]
        public void CommandShouldBeSerializable()
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(new MemoryStream(), new TestCommand());
        }

        [TestMethod]
        public void ResultfulCommandShouldBeSerializable()
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(new MemoryStream(), new TestResultfulCommand());
        }

        [Serializable]
        public class TestCommand : Command
        {
            protected override void Execute()
            {
                throw new NotImplementedException();
            }
        }

        [Serializable]
        public class TestResultfulCommand : Command<bool>
        {
            protected override bool Execute()
            {
                throw new NotImplementedException();
            }
        }
    }
}
