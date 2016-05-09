using System;
using System.Net.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.ExceptionHandling;
using PriceAggregator.Core.Libraries.Logging;

namespace PriceAggregator.Core.ExceptionHandling
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        public override bool ShouldLog(ExceptionLoggerContext context)
        {
            return ShouldHandleExceptionHandler.ShouldHandle(context.Exception);
        }

        public override void Log(ExceptionLoggerContext context)
        {
            IDependencyScope scope = context.Request.GetDependencyScope();
            var logger = (ILoggingService) scope.GetService(typeof (ILoggingService));
            if (logger != null)
                logger.Error(context.Exception,
                    String.Format("{0}\t{1}", context.Exception.Message, context.Exception.StackTrace));
        }
    }
}