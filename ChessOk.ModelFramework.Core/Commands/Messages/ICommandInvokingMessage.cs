using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// Предоставляет инерфейс для сообщения, отправляемого перед выполнением
    /// команд с типом <typeparamref name="T"/> (и всех её наследников).
    /// </summary>
    /// 
    /// <remarks>
    /// Если выполнение команды было отменено с помощью <see cref="CancelInvocation"/>,
    /// то остальные обработчики сообщения <see cref="ICommandInvokingMessage{T}"/> все-равно
    /// будут вызваны, поскольку порядок вызова обработчиков сообщений шины недетерминирован.
    /// 
    /// См. также <see cref="CommandInvokedHandler{T}"/> и <see cref="CommandDispatcher"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">Тип добавляемой в очередь команды.</typeparam>
    public interface ICommandInvokingMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// Получает экземпляр выполняемой команды.
        /// </summary>
        T Command { get; }

        /// <summary>
        /// Получает значение, указывающее, было ли отменено выполнение команды.
        /// </summary>
        bool InvocationCancelled { get; }

        /// <summary>
        /// Отменяет выполнение команды <see cref="Command"/>.
        /// </summary>
        void CancelInvocation();
    }
}