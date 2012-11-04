using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// ������������� ��������� ��� ���������, ����������� � ���� <see cref="IApplicationBus"/> 
    /// ����� ���������� ������� � ����� <typeparamref name="T"/> (� ���� ��� �����������).
    /// </summary>
    /// 
    /// <remarks>
    /// ��. ����� <see cref="CommandInvokedHandler{T}"/> � <see cref="CommandDispatcher"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">��� ����������� �������.</typeparam>
    public interface ICommandInvokedMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// ��������� ����������� �������.
        /// </summary>
        T Command { get; }
    }
}