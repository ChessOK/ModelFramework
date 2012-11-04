using System;

namespace ChessOk.ModelFramework.AsyncCommands.Handlers
{
    /// <summary>
    /// Исключение, служащее оберткой для всех исключений, брошенных
    /// при обработке команды внутри <see cref="IAsyncCommandHandler"/>.
    /// </summary>
    [Serializable]
    public class AsyncCommandHandlingException : Exception
    {
        /// <summary>
        /// Инициализирует экхемпляр класса <see cref="AsyncCommandHandlingException"/>,
        /// используя <paramref name="innerException"/>.
        /// </summary>
        /// <param name="innerException"></param>
        public AsyncCommandHandlingException(Exception innerException)
            : base("An exception has been thrown during handling the message", innerException)
        {
        }
    }
}