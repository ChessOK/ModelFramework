namespace ChessOk.ModelFramework.Messages
{
    /// <summary>
    /// Базовый интерфейс для сообщений, передаваемым по <see cref="IApplicationBus"/>.
    /// </summary>
    public interface IApplicationBusMessage
    {
        /// <summary>
        /// Имя сообщения. На него подписываются обработчики. 
        /// <remarks> При определении имени желательно указывать имя класса сообщения,
        /// чтобы избежать возможных конфликтов.</remarks>
        /// </summary>
        string MessageName { get; }
    }
}
