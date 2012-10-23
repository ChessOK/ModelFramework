using System;
using System.Collections.Generic;

using ChessOk.ModelFramework.Contexts;
using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Шина сообщений приложения предоставляет единый интерфейс
    /// для инициирования всех операций, изменяющих состояние приложения.
    /// </summary>
    public interface IApplicationBus : IDisposable
    {
        /// <summary>
        /// Получить <see cref="IValidationContext"/>, ассоциированный с шиной.
        /// </summary>
        IValidationContext ValidationContext { get; }

        /// <summary>
        /// Получить <see cref="IContext"/>, ассоциированный с шиной.
        /// </summary>
        IContext Context { get; }

        /// <summary>
        /// Получить список зарегистрированных в шине обработчиков сообщений.
        /// </summary>
        IEnumerable<IApplicationBusMessageHandler> Handlers { get; }

        /// <summary>
        /// Провалидировать сообщение, используя <see cref="ObjectValidator"/> 
        /// и вызвать все его обработчики.
        /// </summary>
        /// <param name="message">Передаваемое сообщение</param>
        void Send(IApplicationBusMessage message);

        /// <summary>
        /// Провалидировать сообщение, используя <see cref="ObjectValidator"/>
        /// и вызвать все его обработчики.
        /// Если во время обработки сообщения было выброшено валидационное исключение,
        /// то оно игнорируется и возвращается False.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>False, если было выброшено валидационное исключение. В противном случае True.</returns>
        bool TrySend(IApplicationBusMessage message);
    }
}
