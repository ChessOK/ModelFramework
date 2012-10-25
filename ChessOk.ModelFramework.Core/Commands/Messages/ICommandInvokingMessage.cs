using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// <para>
    /// ������� ������������ �� ������ ������� ���������� ���� ��� ��� ����������. 
    /// </para>
    /// <para>
    /// Ÿ ���������� ����� ��������, �������� ��������������� ��������
    /// � �������� <see cref="InvocationCancelled"/>.
    /// </para>
    /// <typeparam name="T">��� ���������� �������.</typeparam>
    /// </summary>
    public interface ICommandInvokingMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// ��������� ���������� �������.
        /// </summary>
        T Command { get; }

        /// <summary>
        /// �������� ���������� �������. 
        /// <remarks>
        /// ����� ������� ���� ��������, �����, ����� ����� ������ ���� ������������, ���� ���� ����������.
        /// </remarks>
        /// </summary>
        bool InvocationCancelled { get; }
        void CancelInvocation();
    }

    internal class CommandInvokingMessage<T> : ICommandInvokingMessage<T>
    {
        public CommandInvokingMessage(T command)
        {
            Command = command;
        }

        public T Command { get; private set; }
        public bool InvocationCancelled { get; private set; }

        public void CancelInvocation()
        {
            InvocationCancelled = true;
        }

        public string MessageName
        {
            get { return GetMessageName(); }
        }

        public static string GetMessageName()
        {
            return typeof(ICommandInvokingMessage<>).Name;
        }
    }
}