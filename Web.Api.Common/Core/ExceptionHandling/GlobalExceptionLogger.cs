using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using PriceAggergator.Core.Logging.Inteface;

namespace Web.Api.Common.Core.ExceptionHandling
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        public override bool ShouldLog(ExceptionLoggerContext context)
        {
            return ShouldHandleExceptionHandler.ShouldHandle(context.Exception);
        }

        public override void Log(ExceptionLoggerContext context)
        {
            var scope = context.Request.GetDependencyScope();
            var logger = (ILoggingService) scope.GetService(typeof(ILoggingService));
            logger?.Error(context.Exception,
                $"{context.Exception.Message}\t{context.Exception.StackTrace}");
        }
    }
}