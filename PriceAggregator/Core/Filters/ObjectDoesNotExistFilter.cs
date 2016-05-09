using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using PriceAggregator.Core.ExceptionHandling;

namespace PriceAggregator.Core.Filters
{
    public class ObjectDoesNotExistFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is ObjectDoesNotExistException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }
    }
}