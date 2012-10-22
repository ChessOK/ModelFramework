using System;

namespace ChessOk.ModelFramework.Logging
{
    public interface ILog
    {
        /// <summary>
        /// Отладочное сообщение. 
        /// Для разработчика.
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Debug(string message);

        /// <summary>
        /// Отладочное сообщение о произошедшем исключением. 
        /// Для разработчика.
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="ex">Исключение</param>
        void Debug(string message, Exception ex);

        /// <summary>
        /// Информационное сообщение. 
        /// Для администратора.
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Info(string message);

        /// <summary>
        /// Информационное сообщение о произошедшем исключении. 
        /// Для администратора.
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="ex">Исключение</param>
        void Info(string message, Exception ex);

        /// <summary>
        /// Предупреждающее о возможных сбоях сообщение. 
        /// Для администратора.
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Warning(string message);

        /// <summary>
        /// Предупреждающее о возможных сбоях сообщение с исключением.
        /// Для администратора.
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="ex">Исключение</param>
        void Warning(string message, Exception ex);

        /// <summary>
        /// Сообщение о произошедшем сбое, после которого система может продолжить работу.
        /// Для администратора.
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Error(string message);

        /// <summary>
        /// Сообщение о произошедшем сбое с исключением, после которого система может продолжить работу. 
        /// Для администратора.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Error(string message, Exception ex);

        /// <summary>
        /// ВСЕ ПРОПАЛО!!!1!1!!
        /// Система больше не может работать в нормальном режиме и нуждается во вмешательстве администратора.
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Fatal(string message);

        /// <summary>
        /// МЫ ВСЕ УМРЕМ!11!!!!
        /// Система больше не может работать в нормальном режиме и нуждается во вмешательстве администратора.
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="ex">Исключение</param>
        void Fatal(string message, Exception ex);
    }
}
