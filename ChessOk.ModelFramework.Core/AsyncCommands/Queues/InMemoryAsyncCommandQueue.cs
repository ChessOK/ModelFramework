using System;
using System.Collections.Concurrent;

using ChessOk.ModelFramework.Commands.Internals;

namespace ChessOk.ModelFramework.AsyncCommands.Queues
{
    /// <summary>
    /// ������� ���������, ������������� ������ � ������.
    /// ��������� �������� ��� �������� ����������, ��� �� �������
    /// ��� ����� ������ ���������, ����������� � ���������
    /// </summary>
    public sealed class InMemoryAsyncCommandQueue : IAsyncCommandQueue
    {
        private static readonly ConcurrentQueue<CommandBase> _innerQueue = new ConcurrentQueue<CommandBase>();

        /// <summary>
        /// ��������� ��������� � �������
        /// </summary>
        public void Enqueue(CommandBase asyncCommand)
        {
            _innerQueue.Enqueue(asyncCommand);
        }

        /// <summary>
        /// ������ ��������� �� ������� (���� ��� ��� ����).
        /// � ������ ���������� ��������� �������� �������� null.
        /// </summary>
        /// <returns>��������� ��� null (� ������ ��� ����������)</returns>
        public CommandBase Dequeue()
        {
            CommandBase asyncCommand;
            return _innerQueue.TryDequeue(out asyncCommand) ? asyncCommand : null;
        }

        void IDisposable.Dispose()
        {
        }
    }
}