using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    /// <summary>
    /// ������� ������������ ����� �������� ��������� � �������.
    /// </summary>
    /// <typeparam name="T">��� ������������� ���������.</typeparam>
    public interface IAsyncCommandSentMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// ��������� ������������� ���������.
        /// </summary>
        T Command { get; }
    }

    internal class AsyncCommandSentMessage<T> : IAsyncCommandSentMessage<T>
    {
        public AsyncCommandSentMessage(T command)
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
            return typeof(AsyncCommandSentMessage<>).Name;
        }
    }
}