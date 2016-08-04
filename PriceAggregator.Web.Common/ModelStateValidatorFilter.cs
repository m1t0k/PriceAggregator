using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace PriceAggregator.Web.Common
{
    public class ModelStateValidatorFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Model state is not valid");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}