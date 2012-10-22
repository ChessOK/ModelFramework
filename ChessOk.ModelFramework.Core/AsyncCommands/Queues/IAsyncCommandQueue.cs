using System;

using ChessOk.ModelFramework.Commands.Internals;

namespace ChessOk.ModelFramework.AsyncCommands.Queues
{
    /// <summary>
    /// ������� ���������. ���������� ������ ���� ����������������.
    /// </summary>
    public interface IAsyncCommandQueue : IDisposable
    {
        void Enqueue(CommandBase asyncCommand);
        CommandBase Dequeue();
    }
}