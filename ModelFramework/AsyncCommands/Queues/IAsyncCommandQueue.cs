using System;

using ChessOk.ModelFramework.Commands;

namespace ChessOk.ModelFramework.AsyncCommands.Queues
{
    /// <summary>
    /// Предоставляет интерфейс для очереди команд,
    /// выполняющихся асинхронно.
    /// </summary>
    public interface IAsyncCommandQueue : IDisposable
    {
        /// <summary>
        /// Добавить указанную команду <paramref name="asyncCommand"/> 
        /// в конец очереди. Команда должна быть помечена атрибутом <see cref="SerializableAttribute"/>.
        /// </summary>
        /// <param name="asyncCommand">Добавляемая команда.</param>
        void Enqueue(CommandBase asyncCommand);

        /// <summary>
        /// Извлечь команду из начала очереди.
        /// </summary>
        /// <returns>Экземпляр команды, или null, если очередь пуста.</returns>
        CommandBase Dequeue();
    }
}