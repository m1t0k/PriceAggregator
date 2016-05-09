using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using PriceAggregator.Core.ExceptionHandling;

namespace PriceAggregator.Core.Filters
{
    public class ObjectAlreadyExistsFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is ObjectAlreadyExistsException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.Conflict);
            }
        }
    }
}