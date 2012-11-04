using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// ѕредоставл€ет интерфейс дл€ сообщени€, посылаемого в шину <see cref="IApplicationBus"/> 
    /// после выполнени€ команды с типом <typeparamref name="T"/> (и всех его наследников).
    /// </summary>
    /// 
    /// <remarks>
    /// —м. также <see cref="CommandInvokedHandler{T}"/> и <see cref="CommandDispatcher"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">“ип выполненной команды.</typeparam>
    public interface ICommandInvokedMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// Ёкземпл€р выполненной команды.
        /// </summary>
        T Command { get; }
    }
}