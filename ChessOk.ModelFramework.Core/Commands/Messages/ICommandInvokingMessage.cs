using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// ������������� �������� ��� ���������, ������������� ����� �����������
    /// ������ � ����� <typeparamref name="T"/> (� ���� � �����������).
    /// </summary>
    /// 
    /// <remarks>
    /// ���� ���������� ������� ���� �������� � ������� <see cref="CancelInvocation"/>,
    /// �� ��������� ����������� ��������� <see cref="ICommandInvokingMessage{T}"/> ���-�����
    /// ����� �������, ��������� ������� ������ ������������ ��������� ���� ����������������.
    /// 
    /// ��. ����� <see cref="CommandInvokedHandler{T}"/> � <see cref="CommandDispatcher"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">��� ����������� � ������� �������.</typeparam>
    public interface ICommandInvokingMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// �������� ��������� ����������� �������.
        /// </summary>
        T Command { get; }

        /// <summary>
        /// �������� ��������, �����������, ���� �� �������� ���������� �������.
        /// </summary>
        bool InvocationCancelled { get; }

        /// <summary>
        /// �������� ���������� ������� <see cref="Command"/>.
        /// </summary>
        void CancelInvocation();
    }
}