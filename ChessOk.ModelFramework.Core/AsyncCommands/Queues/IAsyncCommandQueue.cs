using System;

using ChessOk.ModelFramework.Commands.Internals;

namespace ChessOk.ModelFramework.AsyncCommands.Queues
{
    /// <summary>
    /// Очередь сообщений. Реализация должна быть потокобезопасной.
    /// </summary>
    public interface IAsyncCommandQueue : IDisposable
    {
        void Enqueue(CommandBase asyncCommand);
        CommandBase Dequeue();
    }
}