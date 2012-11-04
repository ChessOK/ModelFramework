using System;

using ChessOk.ModelFramework.Commands;

namespace ChessOk.ModelFramework.AsyncCommands.Queues
{
    /// <summary>
    /// ������������� ��������� ��� ������� ������,
    /// ������������� ����������.
    /// </summary>
    public interface IAsyncCommandQueue : IDisposable
    {
        /// <summary>
        /// �������� ��������� ������� <paramref name="asyncCommand"/> 
        /// � ����� �������. ������� ������ ���� �������� ��������� <see cref="SerializableAttribute"/>.
        /// </summary>
        /// <param name="asyncCommand">����������� �������.</param>
        void Enqueue(CommandBase asyncCommand);

        /// <summary>
        /// ������� ������� �� ������ �������.
        /// </summary>
        /// <returns>��������� �������, ��� null, ���� ������� �����.</returns>
        CommandBase Dequeue();
    }
}