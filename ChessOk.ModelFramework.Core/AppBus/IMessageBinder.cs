using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Класс, реализующий данный интерфейс можно указывать в качестве
    /// параметров <see cref="ApplicationBusExtensions.Send{T}(IApplicationBus)"/> и
    /// <see cref="ApplicationBusExtensions.TrySend{T}(IApplicationBus)"/> для
    /// большего удобства.
    /// </summary>
    /// <typeparam name="T">Тип сообщения.</typeparam>
    public interface IMessageBinder<in T>
        where T : IApplicationBusMessage
    {
        /// <summary>
        /// Инициализивать сообщение <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Инициализируемое сообщение.</param>
        void Bind(T message);
    }
}
