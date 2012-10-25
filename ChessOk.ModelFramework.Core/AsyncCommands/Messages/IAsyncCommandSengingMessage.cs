using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    /// <summary>
    /// <para>
    /// ������� ������������ �� �������� ��������� � �������.
    /// </para>
    /// <para>
    /// �������� ������� ����� ��������, �������� ��������������� �������� � �������� <see cref="SendingCancelled"/>.
    /// </para>
    /// </summary>
    /// <typeparam name="T">��� ������������� �������.</typeparam>
    public interface IAsyncCommandSendingMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// ��������� ������������� �������.
        /// </summary>
        T Command { get; }

        /// <summary>
        /// �������� �������� ���������.
        /// <remarks>
        /// ����� �������� ���� ��������, �����, ����� ���������� ���� ���������� ����� ������ ���� ������������ �������.
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