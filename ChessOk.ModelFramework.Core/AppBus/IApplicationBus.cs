using System;
using System.Collections.Generic;

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
        /// Получить <see cref="IModelContext"/>, ассоциированный с шиной.
        /// </summary>
        IModelContext Model { get; }

        /// <summary>
        /// Провалидировать сообщение, используя <see cref="ObjectValidator"/> 
        /// и вызвать все его обработчики.
        /// </summary>
        /// <param name="message">Cообщение.</param>
        /// <exception cref="ValidationException">Валидация сообщения завершилась неудачей.</exception>
        void Send(IApplicationBusMessage message);

        /// <summary>
        /// <para>Провалидировать сообщение, используя <see cref="ObjectValidator"/>
        /// и вызвать все его обработчики.</para>
        /// <para>Если во время обработки сообщения было выброшено валидационное исключение,
        /// то оно игнорируется и возвращается False.</para>
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>False, если валидация завершилась неудачей. True в противном случае.</returns>
        bool TrySend(IApplicationBusMessage message);
    }
}
