using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// ������� ������������ ����� ������ ������� ���������� ���� ��� ��� ����������.
    /// </summary>
    /// <typeparam name="T">��� ��������� �������.</typeparam>
    public interface ICommandInvokedMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// ��������� ��������� �������.
        /// </summary>
        T Command { get; }
    }

    internal class CommandInvokedMessage<T> : ICommandInvokedMessage<T>
    {
        public CommandInvokedMessage(T command)
        {
            Command = command;
        }

        public T Command { get; private set; }
    }
}