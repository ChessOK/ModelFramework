using System;

using ChessOk.ModelFramework.Commands.Internals;
using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework.AsyncCommands
{
    public class AsyncCommand : IApplicationBusMessage
    {
        public AsyncCommand(CommandBase command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            Command = command;
        }

        [Valid]
        public CommandBase Command { get; private set; }
    }
}
