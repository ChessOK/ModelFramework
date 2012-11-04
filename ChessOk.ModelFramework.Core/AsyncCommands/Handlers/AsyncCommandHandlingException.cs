using System;

namespace ChessOk.ModelFramework.AsyncCommands.Handlers
{
    /// <summary>
    /// ����������, �������� �������� ��� ���� ����������, ���������
    /// ��� ��������� ������� ������ <see cref="IAsyncCommandHandler"/>.
    /// </summary>
    [Serializable]
    public class AsyncCommandHandlingException : Exception
    {
        /// <summary>
        /// �������������� ��������� ������ <see cref="AsyncCommandHandlingException"/>,
        /// ��������� <paramref name="innerException"/>.
        /// </summary>
        /// <param name="innerException"></param>
        public AsyncCommandHandlingException(Exception innerException)
            : base("An exception has been thrown during handling the message", innerException)
        {
        }
    }
}