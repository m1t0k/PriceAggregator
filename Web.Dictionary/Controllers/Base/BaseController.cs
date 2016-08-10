using System.Web.Http;

namespace Web.Dictionary.Controllers.Base
{
    public abstract class BaseController : ApiController
    {
        protected IDynamicExecutorHelper DynamicExecutor;

        protected BaseController(IDynamicExecutorHelper dynamicExecutor)
        {
            DynamicExecutor = dynamicExecutor;
        }
    }
}