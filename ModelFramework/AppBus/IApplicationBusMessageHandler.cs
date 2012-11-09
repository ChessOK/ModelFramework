using System.Collections.Generic;

namespace ChessOk.ModelFramework.Messages
{
    /// <summary>
    /// Интерфейс для обработчиков сообщений.
    /// </summary>
    public interface IApplicationBusMessageHandler
    {
        /// <summary>
        /// Обработать сообщение, указанное в параметрах. На обработку могут прийти любые
        /// сообщения с одним из имен, указанных в <see cref="MessageNames"/>.
        /// </summary>
        /// <param name="ev">Обрабатываемое сообщение</param>
        void Handle(IApplicationBusMessage ev);

        /// <summary>
        /// Имена всех обрабатываемых данным обработчиком сообщений.
        /// </summary>
        IEnumerable<string> MessageNames { get; }
    }
}
