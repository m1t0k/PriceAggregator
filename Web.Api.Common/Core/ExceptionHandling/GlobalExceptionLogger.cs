using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
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

        
        public override Task LogAsync(
            ExceptionLoggerContext context,
            System.Threading.CancellationToken cancellationToken)
        {
            Log(context);
            return Task.FromResult(0);
        }

        public override void Log(ExceptionLoggerContext context)
        {
            try
            {
                var scope = context.Request.GetDependencyScope();
                var logger = (ILoggingService) scope.GetService(typeof(ILoggingService));
                logger?.Error(context.Exception,
                    $"{context.Exception.Message}\t{context.Exception.StackTrace}");
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
            }
        }
    }
}