using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    /// <summary>
    /// Предоставляет интерфейс для сообщения, посылаемого в шину <see cref="IApplicationBus"/> 
    /// после добавления команды с типом <typeparamref name="T"/> (и всех его наследников) в очередь.
    /// </summary>
    /// 
    /// <remarks>
    /// См. также <see cref="AsyncCommandEnqueuedHandler{T}"/> и <see cref="AsyncCommandDispatcher"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">Тип добавленной в очередь команды.</typeparam>
    public interface IAsyncCommandEnqueuedMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// Экземпляр добавленной в очередь команды.
        /// </summary>
        T Command { get; }
    }

    internal class AsyncCommandEnqueuedMessage<T> : IAsyncCommandEnqueuedMessage<T>
    {
        public AsyncCommandEnqueuedMessage(T command)
        {
            Command = command;
        }

        public T Command { get; private set; }

        public string MessageName
        {
            get { return GetMessageName(); }
        }

        public static string GetMessageName()
        {
            return typeof(AsyncCommandEnqueuedMessage<>).Name;
        }
    }
}