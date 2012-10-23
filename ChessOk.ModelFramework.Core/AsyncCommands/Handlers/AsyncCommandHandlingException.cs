using System;

namespace ChessOk.ModelFramework.AsyncCommands.Handlers
{
    public class AsyncCommandHandlingException : Exception
    {
        public AsyncCommandHandlingException(Exception innerException)
            : base("An exception has been thrown during handling the message", innerException)
        {
        }
    }
}