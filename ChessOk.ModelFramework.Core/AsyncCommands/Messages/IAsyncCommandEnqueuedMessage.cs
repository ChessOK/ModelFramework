using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    /// <summary>
    /// ѕредоставл€ет интерфейс дл€ сообщени€, посылаемого в шину <see cref="IApplicationBus"/> 
    /// после добавлени€ команды с типом <typeparamref name="T"/> (и всех его наследников) в очередь.
    /// </summary>
    /// 
    /// <remarks>
    /// —м. также <see cref="AsyncCommandEnqueuedHandler{T}"/> и <see cref="AsyncCommandDispatcher"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">“ип добавленной в очередь команды.</typeparam>
    public interface IAsyncCommandEnqueuedMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// Ёкземпл€р добавленной в очередь команды.
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