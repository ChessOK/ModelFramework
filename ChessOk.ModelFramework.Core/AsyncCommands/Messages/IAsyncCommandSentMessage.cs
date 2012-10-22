using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    /// <summary>
    /// ������� ������������ ����� �������� ��������� � �������.
    /// </summary>
    /// <typeparam name="T">��� ������������� ���������.</typeparam>
    public interface IAsyncCommandSentMessage<out T> : IApplicationMessage
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
    }
}