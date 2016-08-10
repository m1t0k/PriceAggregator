using System;
using System.Threading.Tasks;
using NLog;
using PriceAggergator.Core.Logging.Inteface;

namespace PriceAggregator.Core.Logging
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
            AsyncWrapper(delegate { Logger.Debug(format, args); });
        }

        public void Debug(Exception exception, string format, params object[] args)
        {
            if (!Logger.IsDebugEnabled) return;

            AsyncWrapper(delegate
            {
                var logEvent = GetLogEvent(LogLevel.Debug, exception, format, args);
                Logger.Log(typeof (NLogLoggingService), logEvent);
            });
        }

        public void Error(string format, params object[] args)
        {
            AsyncWrapper(delegate { Logger.Error(format, args); });
        }


        public void Error(Exception exception, string format, params object[] args)
        {
            if (!Logger.IsErrorEnabled) return;

            AsyncWrapper(delegate
            {
                var logEvent = GetLogEvent(LogLevel.Error, exception, format, args);
                Logger.Log(typeof (NLogLoggingService), logEvent);
            });
        }

        public void Fatal(string format, params object[] args)
        {
            AsyncWrapper(delegate { Logger.Fatal(format, args); });
        }

        public void Fatal(Exception exception, string format, params object[] args)
        {
            if (!Logger.IsFatalEnabled) return;

            AsyncWrapper(delegate
            {
                var logEvent = GetLogEvent(LogLevel.Fatal, exception, format, args);
                Logger.Log(typeof (NLogLoggingService), logEvent);
            });
        }

        public void Info(string format, params object[] args)
        {
            AsyncWrapper(delegate { Logger.Info(format, args); });
        }

        public void Info(Exception exception, string format, params object[] args)
        {
            if (!Logger.IsInfoEnabled) return;

            AsyncWrapper(delegate
            {
                var logEvent = GetLogEvent(LogLevel.Info, exception, format, args);
                Logger.Log(typeof (NLogLoggingService), logEvent);
            });
        }

        public void Trace(string format, params object[] args)
        {
            AsyncWrapper(delegate { Logger.Trace(format, args); });
        }

        public void Trace(Exception exception, string format, params object[] args)
        {
            if (!Logger.IsTraceEnabled) return;
            AsyncWrapper(delegate
            {
                var logEvent = GetLogEvent(LogLevel.Trace, exception, format, args);
                Logger.Log(typeof (NLogLoggingService), logEvent);
            });
        }

        public void Warn(string format, params object[] args)
        {
            AsyncWrapper(delegate { Logger.Warn(format, args); });
        }

        public void Warn(Exception exception, string format, params object[] args)
        {
            if (!Logger.IsWarnEnabled) return;
            AsyncWrapper(delegate
            {
                var logEvent = GetLogEvent(LogLevel.Warn, exception, format, args);
                Logger.Log(typeof (NLogLoggingService), logEvent);
            });
        }

        public bool IsDebugEnabled => Logger.IsDebugEnabled;

        public bool IsErrorEnabled => Logger.IsErrorEnabled;

        public bool IsFatalEnabled => Logger.IsFatalEnabled;

        public bool IsInfoEnabled => Logger.IsInfoEnabled;

        public bool IsTraceEnabled => Logger.IsTraceEnabled;

        public bool IsWarnEnabled => Logger.IsWarnEnabled;

        public void Debug(Exception exception)
        {
            Debug(exception, string.Empty);
        }

        public void Error(Exception exception)
        {
            AsyncWrapper(delegate { Logger.Error(exception); });
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

        public void Dispose()
        {
        }

        private static async void AsyncWrapper(LoggerDelagate func)
        {
            await Task.Factory.StartNew(() =>
            {
                func();
                return true;
            });
        }

        private void ExceptionHandling(Task task)
        {
            // to do fix this later
        }

        private LogEventInfo GetLogEvent(LogLevel level, Exception exception, string format, object[] args)
        {
            var assemblyProp = string.Empty;
            var classProp = string.Empty;
            var methodProp = string.Empty;
            var messageProp = string.Empty;
            var innerMessageProp = string.Empty;

            var logEvent = new LogEventInfo
                (level, "*", string.Format(format, args));

            if (exception != null)
            {
                assemblyProp = exception.Source;
                if (exception.TargetSite.DeclaringType != null) classProp = exception.TargetSite.DeclaringType.FullName;
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

        private delegate void LoggerDelagate();
    }
}