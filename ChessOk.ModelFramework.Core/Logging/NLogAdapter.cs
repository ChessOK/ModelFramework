using System;

using NLog;

namespace ChessOk.ModelFramework.Logging.NLog
{
    public class NLogAdapter : ILog
    {
        private readonly Logger _logger;

        public NLogAdapter(Logger logger)
        {
            _logger = logger;
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string message, Exception ex)
        {
            _logger.DebugException(message, ex);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(string message, Exception ex)
        {
            _logger.InfoException(message, ex);
        }

        public void Warning(string message)
        {
            _logger.Warn(message);
        }

        public void Warning(string message, Exception ex)
        {
            _logger.WarnException(message, ex);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            _logger.ErrorException(message, ex);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(string message, Exception ex)
        {
            _logger.FatalException(message, ex);
        }
    }
}
