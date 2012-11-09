using System;
using System.Collections.Concurrent;

using ChessOk.ModelFramework.Commands;

namespace ChessOk.ModelFramework.AsyncCommands.Queues
{
    /// <summary>
    /// Очередь команд, располагаемая в оперативной памяти.
    /// </summary>
    public sealed class InMemoryAsyncCommandQueue : IAsyncCommandQueue
    {
        private static readonly ConcurrentQueue<CommandBase> _innerQueue = new ConcurrentQueue<CommandBase>();

        public void Enqueue(CommandBase asyncCommand)
        {
            _innerQueue.Enqueue(asyncCommand);
        }

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