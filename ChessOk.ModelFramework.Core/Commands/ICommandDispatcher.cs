using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands
{
    /// <summary>
    /// Предоставляет интерфейс для обработчиков сообщений типа <see cref="CommandBase"/>.
    /// <para>Служит для регистрации собственных обработчиков команд вместо обработчика
    /// по-умолчанию <see cref="CommandDispatcher"/>.</para>
    /// </summary>
    public interface ICommandDispatcher : IApplicationBusMessageHandler
    {
    }
}
