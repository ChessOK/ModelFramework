using System;

namespace ChessOk.ModelFramework.AsyncCommands.Handlers
{
    /// <summary>
    /// »сключение, служащее оберткой дл€ всех исключений, брошенных
    /// при обработке команды внутри <see cref="IAsyncCommandHandler"/>.
    /// </summary>
    [Serializable]
    public class AsyncCommandHandlingException : Exception
    {
        /// <summary>
        /// »нициализирует экхемпл€р класса <see cref="AsyncCommandHandlingException"/>,
        /// использу€ <paramref name="innerException"/>.
        /// </summary>
        /// <param name="innerException"></param>
        public AsyncCommandHandlingException(Exception innerException)
            : base("An exception has been thrown during handling the message", innerException)
        {
        }
    }
}