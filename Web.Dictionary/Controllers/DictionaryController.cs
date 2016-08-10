using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Web.Api.Common.Core.Filters;
using Web.Api.Common.Core.Formatters;
using Web.Dictionary.Controllers.Base;

namespace Web.Dictionary.Controllers
{
    [RoutePrefix("api/dictionary")]
    [Authorize]
    public class DictionaryController : BaseController
    {
        public DictionaryController(IDynamicExecutorHelper dynamicExecutor) : base(dynamicExecutor)
        {
        }

        [Route("types")]
        [HttpGet]
        public IHttpActionResult GetTypes()
        {
            var list = DynamicExecutor.GetSupportedTypes(Configuration);

            return Ok(list.Select(t => t.Name));
        }


        [Route("{typeName:alpha}/list")]
        [HttpGet]
        public HttpResponseMessage GetList(string typeName)

        {
            return DynamicExecutor.ExecuteWithFormatter(Configuration, Request, typeName, "GetList",
                new object[] {null, null, null});
        }


        [Route("{typeName:alpha}/list/csv")]
        [HttpGet]
        public HttpResponseMessage GetCsvList(string typeName)

        {
            var formatter = Configuration.Formatters.FirstOrDefault(item => item.GetType() == typeof (CsvFormater));
            return DynamicExecutor.ExecuteWithFormatter(Configuration, Request, typeName, "GetList",
                new object[] {null, null, null}, null, formatter);
        }

        [Route("{typeName:alpha}/list/{pageIndex:int}")]
        [HttpGet]
        public HttpResponseMessage GetList(string typeName, int pageIndex)
        {
            return DynamicExecutor.ExecuteWithFormatter(Configuration, Request, typeName, "GetList",
                new object[] {pageIndex, null, null});
        }

        [Route("{typeName:alpha}/list/{pageIndex:int}/{pageSize:int}")]
        [HttpGet]
        public HttpResponseMessage GetList(string typeName, int pageIndex, int pageSize)
        {
            return DynamicExecutor.ExecuteWithFormatter(Configuration, Request, typeName, "GetList",
                new object[] {pageIndex, pageSize, null});
        }

        [Route("{typeName:alpha}/list/{pageIndex:int}/{pageSize:int}/{sortName}")]
        [HttpGet]
        public HttpResponseMessage GetList(string typeName, int? pageIndex, int? pageSize = null,
            string sortName = null)
        {
            return DynamicExecutor.ExecuteWithFormatter(Configuration, Request, typeName, "GetList",
                new object[] {pageIndex, pageSize, sortName});
        }

        [Route("{typeName:alpha}/list/count")]
        [HttpGet]
        public HttpResponseMessage GetCount(string typeName)
        {
            return DynamicExecutor.ExecuteWithFormatter(Configuration, Request, typeName, "GetCount", null);
        }


        [Route("{typeName:alpha}/{id:int}")]
        [HttpGet]
        [ObjectDoesNotExistFilter]
        public HttpResponseMessage GetItem(string typeName, int id)
        {
            return DynamicExecutor.ExecuteWithFormatter(Configuration, Request, typeName, "GetItem", new object[] {id});
        }

        [Route("{typeName:alpha}")]
        [HttpPost]
        [ObjectAlreadyExistsFilter]
        public HttpResponseMessage Post(string typeName, [FromBody] object item)
        {
            return DynamicExecutor.ExecuteWithFormatter(Configuration, Request, typeName, "CreateItem", null, item);
        }


        [Route("{typeName:alpha}/{id:int}")]
        [HttpPut]
        [ObjectDoesNotExistFilter]
        public HttpResponseMessage Put(string typeName, int id, [FromBody] object item)
        {
            return DynamicExecutor.ExecuteWithFormatter(Configuration, Request, typeName, "UpdateItem", null, item);
        }

        [Route("{typeName:alpha}/{id:int}")]
        [HttpDelete]
        [ObjectDoesNotExistFilter]
        public HttpResponseMessage Delete(string typeName, int id)
        {
            return DynamicExecutor.ExecuteWithFormatter(Configuration, Request, typeName, "DeleteItem", new object[] {id});
        }
    }
}