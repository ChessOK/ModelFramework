using System;

namespace ChessOk.ModelFramework.AsyncCommads.Handlers
{
    public class AsyncCommandHandlingException : Exception
    {
        public AsyncCommandHandlingException(Exception innerException)
            : base("An exception has been thrown during handling the message", innerException)
        {
        }
    }
}