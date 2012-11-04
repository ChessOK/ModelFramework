using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    /// <summary>
    /// ������������� ��������� ��� ���������, ����������� � ���� <see cref="IApplicationBus"/> 
    /// ����� ���������� ������� � ����� <typeparamref name="T"/> (� ���� ��� �����������) � �������.
    /// </summary>
    /// 
    /// <remarks>
    /// ��. ����� <see cref="AsyncCommandEnqueuedHandler{T}"/> � <see cref="AsyncCommandDispatcher"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">��� ����������� � ������� �������.</typeparam>
    public interface IAsyncCommandEnqueuedMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// ��������� ����������� � ������� �������.
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