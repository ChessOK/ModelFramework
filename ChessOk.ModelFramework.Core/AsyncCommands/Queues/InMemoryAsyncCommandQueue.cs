using System;
using System.Collections.Concurrent;

using ChessOk.ModelFramework.Commands.Internals;

namespace ChessOk.ModelFramework.AsyncCommands.Queues
{
    /// <summary>
    /// Очередь сообщений, располагаемая только в памяти.
    /// Сообщения теряются при рестарте приложения, что не годится
    /// для очень важных сообщений, нуждающихся в обработке
    /// </summary>
    public sealed class InMemoryAsyncCommandQueue : IAsyncCommandQueue
    {
        private static readonly ConcurrentQueue<CommandBase> _innerQueue = new ConcurrentQueue<CommandBase>();

        /// <summary>
        /// Поставить сообщение в очередь
        /// </summary>
        public void Enqueue(CommandBase asyncCommand)
        {
            _innerQueue.Enqueue(asyncCommand);
        }

        /// <summary>
        /// Изъять сообщение из очереди (если оно там было).
        /// В случае отсутствия сообщений вернется значение null.
        /// </summary>
        /// <returns>Сообщение или null (в случае его отсутствия)</returns>
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