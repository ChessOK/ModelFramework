using System;
using System.Diagnostics;

using ChessOk.ModelFramework.Logging.NLog;

namespace ChessOk.ModelFramework.Logging
{
    public static class LogManager
    {
        public static ILog Get()
        {
#if SILVERLIGHT
            StackFrame frame = new StackTrace().GetFrame(1);
#else
            var frame = new StackFrame(1, false);
#endif

            var declaringType = frame.GetMethod().DeclaringType;
            var loggerName = declaringType != null ? declaringType.FullName : String.Empty;

            return new NLogAdapter(global::NLog.LogManager.GetLogger(loggerName));
        }
    }
}
