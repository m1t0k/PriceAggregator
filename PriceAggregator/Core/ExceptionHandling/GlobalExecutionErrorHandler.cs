using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace PriceAggregator.Core.ExceptionHandling
{
    public class GlobalExecutionErrorHandler : ExceptionHandler
    {
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return ShouldHandleExceptionHandler.ShouldHandle(context.Exception);
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            var errorData = new ErrorData
            {
                ErrorDateTime = DateTime.UtcNow,
                ErrorMessage = "Iternal Server Error",
                RequestedUri = context.Request.RequestUri
            };

            HttpResponseMessage response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, errorData);
            context.Result = new ResponseMessageResult(response);
        }
    }
}