using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    /// <summary>
    /// <para>
    /// Событие инициируется ДО отправки сообщения в очередь.
    /// </para>
    /// <para>
    /// Отправку события можно отменить, выставив соответствующее значение в свойстве <see cref="SendingCancelled"/>.
    /// </para>
    /// </summary>
    /// <typeparam name="T">Тип отправляемого события.</typeparam>
    public interface IAsyncCommandSendingMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// Экземпляр отправляемого события.
        /// </summary>
        T Command { get; }

        /// <summary>
        /// Отменить отправку сообщения.
        /// <remarks>
        /// Чтобы отправка была отменена, нужно, чтобы включенный флаг сохранился после вызова всех обработчиков события.
        /// </remarks>
        /// </summary>
        bool SendingCancelled { get; }
        void CancelSending();
    }

    internal class AsyncCommandSendingMessage<T> : IAsyncCommandSendingMessage<T>
    {
        public AsyncCommandSendingMessage(T command)
        {
            Command = command;
        }

        public T Command { get; private set; }
        public bool SendingCancelled { get; private set; }

        public void CancelSending()
        {
            SendingCancelled = true;
        }

        public string MessageName
        {
            get { return GetMessageName(); }
        }

        public static string GetMessageName()
        {
            return typeof(IAsyncCommandSendingMessage<>).Name;
        }
    }
}