using System;

namespace ChessOk.ModelFramework.Logging
{
    /// <summary>
    /// Предоставляет интерфейс для ведения лога.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Записывает в лог отладочное сообщение <paramref name="message"/>,
        /// предназначенное для разработчиков приложения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void Debug(string message);

        /// <summary>
        /// Записывает в лог отладочное сообщение <paramref name="message"/> 
        /// о произошедшем исключении <paramref name="exception"/>, 
        /// предназначенное для разработчиков приложения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="exception">Исключение.</param>
        void Debug(string message, Exception exception);

        /// <summary>
        /// Записывает в лог информационное сообщение <paramref name="message"/>, 
        /// предназначенное для администраторов приложения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void Info(string message);

        /// <summary>
        /// Записывает в лог информационное сообщение <paramref name="message"/> 
        /// о произошедшем исключении <paramref name="exception"/>, 
        /// предназначенное для администраторов приложения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="exception">Исключение.</param>
        void Info(string message, Exception exception);

        /// <summary>
        /// Записывает в лог сообщение <paramref name="message"/>, 
        /// предупреждающее о возможных сбоях в системе, предназначенное для 
        /// администраторов приложения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void Warning(string message);

        /// <summary>
        /// Записывает в лог сообщение <paramref name="message"/>, 
        /// предупреждающее о возможных сбоях в системе с приведенным в
        /// <see cref="exception"/> исключении, предназначенное для 
        /// администраторов приложения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="exception">Исключение.</param>
        void Warning(string message, Exception exception);

        /// <summary>
        /// Записывает в лог сообщение <paramref name="message"/>, 
        /// о произошедшем сбое, после которого система может продолжить работу, 
        /// предназначенное для администраторов приложения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void Error(string message);

        /// <summary>
        /// Записывает в лог сообщение <paramref name="message"/>, 
        /// о произошедшем сбое, после которого система может продолжить работу,
        /// с приведенным в <paramref name="exception"/> исключением, предназначенное для 
        /// администраторов приложения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="exception">Исключение.</param>
        void Error(string message, Exception exception);

        /// <summary>
        /// Записывает в лог сообщение <paramref name="message"/>, 
        /// о произошедшем сбое, после которого система не может продолжить работу и
        /// требуется немедленное вмешательство администраторов приложения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void Fatal(string message);

        /// <summary>
        /// Записывает в лог сообщение <paramref name="message"/>, 
        /// о произошедшем сбое с приведенным исключением <paramref name="exception"/>, 
        /// после которого система не может продолжить работу и требуется немедленное 
        /// вмешательство администраторов приложения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="exception">Исключение.</param>
        void Fatal(string message, Exception exception);
    }
}
