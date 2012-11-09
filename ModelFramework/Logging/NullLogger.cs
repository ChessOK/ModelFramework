using System;

namespace ChessOk.ModelFramework.Logging
{
    /// <summary>
    /// Класс-заглушка интерфейса <see cref="ILogger"/>, не делающий
    /// абсолютно ничего.
    /// </summary>
    public class NullLogger : ILogger
    {
        public void Debug(string message)
        {
        }

        public void Debug(string message, Exception exception)
        {
        }

        public void Info(string message)
        {
        }

        public void Info(string message, Exception exception)
        {
        }

        public void Warning(string message)
        {
        }

        public void Warning(string message, Exception exception)
        {
        }

        public void Error(string message)
        {
        }

        public void Error(string message, Exception exception)
        {
        }

        public void Fatal(string message)
        {
        }

        public void Fatal(string message, Exception exception)
        {
        }
    }
}
