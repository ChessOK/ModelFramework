using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    /// <summary>
    /// Предоставляет инерфейс для сообщения, отправляемого перед добавлением
    /// команды с типом <typeparamref name="T"/> (и всех её наследников) в очередь асинхронных команд.
    /// </summary>
    /// 
    /// <remarks>
    /// Если добавление команды в очередь было отменено с помощью <see cref="CancelEnqueuing"/>,
    /// то остальные обработчики сообщения <see cref="IAsyncCommandEnqueuingMessage{T}"/> все-равно
    /// будут вызваны, поскольку порядок вызова обработчиков сообщений шины недетерминирован.
    /// 
    /// См. также <see cref="AsyncCommandEnqueuingHandler{T}"/> и <see cref="AsyncCommandDispatcher"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">Тип добавляемой в очередь команды.</typeparam>
    public interface IAsyncCommandEnqueuingMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// Получает экземпляр добавляемой в очередь команды.
        /// </summary>
        T Command { get; }

        /// <summary>
        /// Получает значение, указывающее, было ли отменено добавление
        /// команды в очередь.
        /// </summary>
        bool EnqueuingCancelled { get; }

        /// <summary>
        /// Отменяет добавление команды <see cref="Command"/> в очередь.
        /// </summary>
        void CancelEnqueuing();
    }

    internal class AsyncCommandEnqueuingMessage<T> : IAsyncCommandEnqueuingMessage<T>
    {
        public AsyncCommandEnqueuingMessage(T command)
        {
            Command = command;
        }

        public T Command { get; private set; }
        public bool EnqueuingCancelled { get; private set; }

        public void CancelEnqueuing()
        {
            EnqueuingCancelled = true;
        }

        public string MessageName
        {
            get { return GetMessageName(); }
        }

        public static string GetMessageName()
        {
            return typeof(IAsyncCommandEnqueuingMessage<>).Name;
        }
    }
}