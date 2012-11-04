using ChessOk.ModelFramework.AsyncCommands.Workers;
using ChessOk.ModelFramework.Commands;

namespace ChessOk.ModelFramework.AsyncCommands.Handlers
{
    /// <summary>
    /// Предоставляет интерфейс для обработчиков команд <see cref="CommandBase"/>,
    /// выполняемых асинхронно.
    /// <para>
    /// Обработчик вызывается после получания команды из очереди (см.
    /// пример в классе <see cref="BackgroundThreadWorker"/>).
    /// </para>
    /// </summary>
    public interface IAsyncCommandHandler
    {
        /// <summary>
        /// Обработать и выполнить команду <paramref name="asyncCommand"/>.
        /// </summary>
        void Handle(CommandBase asyncCommand);
    }
}