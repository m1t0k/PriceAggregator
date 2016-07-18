using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using PriceAggergator.Core.Logging.Inteface;

namespace Web.Api.Common.Core.Filters
{
    public class ValidateModelFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Method != HttpMethod.Post && actionContext.Request.Method != HttpMethod.Put)
                return;
            if (actionContext.ModelState.IsValid) return;
            var logger =
                actionContext.Request.GetDependencyScope().GetService(typeof(ILoggingService)) as ILoggingService;
            logger?.Error(actionContext.ModelState.ToString());

            actionContext.Response = actionContext.Request.CreateErrorResponse(
                HttpStatusCode.BadRequest, actionContext.ModelState);
        }
    }
}