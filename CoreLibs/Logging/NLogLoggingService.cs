using System;
using System.Runtime.CompilerServices;
using NLog;

namespace PriceAggregator.Core.Libraries.Logging
{
    public class NLogLoggingService : ILoggingService
    {
        private static readonly Logger Logger;

        static NLogLoggingService()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }


        public void Debug(string format, params object[] args)
        {
            Logger.Debug(format, args);
        }

        public void Debug(Exception exception, string format, params object[] args)
        {
            if (!Logger.IsDebugEnabled) return;
            LogEventInfo logEvent = GetLogEvent(LogLevel.Debug, exception, format, args);
            Logger.Log(typeof (NLogLoggingService), logEvent);
        }

        public void Error(string format, params object[] args)
        {
            Logger.Error(format, args);
        }
        

        public void Error(Exception exception, string format, params object[] args)
        {
            if (!Logger.IsErrorEnabled) return;
            LogEventInfo logEvent = GetLogEvent(LogLevel.Error, exception, format, args);
            Logger.Log(typeof (NLogLoggingService), logEvent);
        }

        public void Fatal(string format, params object[] args)
        {
            Logger.Fatal(format, args);
        }

        public void Fatal(Exception exception, string format, params object[] args)
        {
            if (!Logger.IsFatalEnabled) return;
            LogEventInfo logEvent = GetLogEvent(LogLevel.Fatal, exception, format, args);
            Logger.Log(typeof (NLogLoggingService), logEvent);
        }

        public void Info(string format, params object[] args)
        {
            Logger.Info(format, args);
        }

        public void Info(Exception exception, string format, params object[] args)
        {
            if (!Logger.IsInfoEnabled) return;
            LogEventInfo logEvent = GetLogEvent(LogLevel.Info, exception, format, args);
            Logger.Log(typeof (NLogLoggingService), logEvent);
        }

        public void Trace(string format, params object[] args)
        {
            Logger.Trace(format, args);
        }

        public void Trace(Exception exception, string format, params object[] args)
        {
            if (!Logger.IsTraceEnabled) return;
            LogEventInfo logEvent = GetLogEvent(LogLevel.Trace, exception, format, args);
            Logger.Log(typeof (NLogLoggingService), logEvent);
        }

        public void Warn(string format, params object[] args)
        {
            Logger.Warn(format, args);
        }

        public void Warn(Exception exception, string format, params object[] args)
        {
            if (!Logger.IsWarnEnabled) return;
            LogEventInfo logEvent = GetLogEvent(LogLevel.Warn, exception, format, args);
            Logger.Log(typeof (NLogLoggingService), logEvent);
        }

        public bool IsDebugEnabled
        {
            get { return Logger.IsDebugEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return Logger.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return Logger.IsFatalEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return Logger.IsInfoEnabled; }
        }

        public bool IsTraceEnabled
        {
            get { return Logger.IsTraceEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return Logger.IsWarnEnabled; }
        }

        public void Debug(Exception exception)
        {
            Debug(exception, string.Empty);
        }

        public void Error(Exception exception)
        {
            Logger.Error(exception);
        }

        public void Fatal(Exception exception)
        {
            Fatal(exception, string.Empty);
        }

        public void Info(Exception exception)
        {
            Info(exception, string.Empty);
        }

        public void Trace(Exception exception)
        {
            Trace(exception, string.Empty);
        }

        public void Warn(Exception exception)
        {
            Warn(exception, string.Empty);
        }


        private LogEventInfo GetLogEvent(LogLevel level, Exception exception, string format, object[] args)
        {
            string assemblyProp = string.Empty;
            string classProp = string.Empty;
            string methodProp = string.Empty;
            string messageProp = string.Empty;
            string innerMessageProp = string.Empty;

            var logEvent = new LogEventInfo
                (level, "*", string.Format(format, args));

            if (exception != null)
            {
                assemblyProp = exception.Source;
                classProp = exception.TargetSite.DeclaringType.FullName;
                methodProp = exception.TargetSite.Name;
                messageProp = exception.Message;

                if (exception.InnerException != null)
                {
                    innerMessageProp = exception.InnerException.Message;
                }
            }

            logEvent.Properties["error-source"] = assemblyProp;
            logEvent.Properties["error-class"] = classProp;
            logEvent.Properties["error-method"] = methodProp;
            logEvent.Properties["error-message"] = messageProp;
            logEvent.Properties["inner-error-message"] = innerMessageProp;

            return logEvent;
        }
    }
}