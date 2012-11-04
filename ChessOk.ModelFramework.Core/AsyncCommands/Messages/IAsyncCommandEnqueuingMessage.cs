using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    /// <summary>
    /// ������������� �������� ��� ���������, ������������� ����� �����������
    /// ������� � ����� <typeparamref name="T"/> (� ���� � �����������) � ������� ����������� ������.
    /// </summary>
    /// 
    /// <remarks>
    /// ���� ���������� ������� � ������� ���� �������� � ������� <see cref="CancelEnqueuing"/>,
    /// �� ��������� ����������� ��������� <see cref="IAsyncCommandEnqueuingMessage{T}"/> ���-�����
    /// ����� �������, ��������� ������� ������ ������������ ��������� ���� ����������������.
    /// 
    /// ��. ����� <see cref="AsyncCommandEnqueuingHandler{T}"/> � <see cref="AsyncCommandDispatcher"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">��� ����������� � ������� �������.</typeparam>
    public interface IAsyncCommandEnqueuingMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// �������� ��������� ����������� � ������� �������.
        /// </summary>
        T Command { get; }

        /// <summary>
        /// �������� ��������, �����������, ���� �� �������� ����������
        /// ������� � �������.
        /// </summary>
        bool EnqueuingCancelled { get; }

        /// <summary>
        /// �������� ���������� ������� <see cref="Command"/> � �������.
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