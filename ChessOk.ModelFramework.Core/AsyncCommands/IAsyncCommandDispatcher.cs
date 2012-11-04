using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands
{
    /// <summary>
    /// Предоставляет интерфейс для обработчиков сообщений типа <see cref="AsyncCommand"/>.
    /// Служит для регистрации собственных обработчиков асинхронных команд вместо обработчика
    /// по-умолчанию <see cref="AsyncCommandDispatcher"/>.
    /// </summary>
    public interface IAsyncCommandDispatcher : IApplicationBusMessageHandler
    {
    }
}
