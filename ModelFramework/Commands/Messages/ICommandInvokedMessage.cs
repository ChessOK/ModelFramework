using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// Предоставляет интерфейс для сообщения, посылаемого в шину <see cref="IApplicationBus"/> 
    /// после выполнения команды с типом <typeparamref name="T"/> (и всех его наследников).
    /// </summary>
    /// 
    /// <remarks>
    /// См. также <see cref="CommandInvokedHandler{T}"/> и <see cref="CommandDispatcher"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">Тип выполненной команды.</typeparam>
    public interface ICommandInvokedMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// Экземпляр выполненной команды.
        /// </summary>
        T Command { get; }
    }
}