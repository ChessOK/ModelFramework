using System;

using ChessOk.ModelFramework.Commands.Internals;
using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework.AsyncCommands
{
    public class AsyncCommandWrapperMessage : IApplicationMessage
    {
        public AsyncCommandWrapperMessage(CommandBase command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            Command = command;
        }

        [ValidateObject]
        public CommandBase Command { get; private set; }
    }
}
