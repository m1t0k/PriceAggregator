using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using PriceAggregator.Core.DataEntity.Base;
using Web.Api.Common.Core.Filters;
using Web.Api.Common.Core.Formatters;
using Web.Dictionary.Controllers.Base;

namespace Web.Dictionary.Controllers
{
    [EnableCors("*", "*", "*", "*", SupportsCredentials = true)]
    [RoutePrefix("api/dictionary")]
    //[Authorize]
    public class DictionaryController : BaseController
    {
        private HttpResponseMessage DynamicExecute(string typeName, string methodName, object[] parameters,
            object dictionaryItem = null, MediaTypeFormatter formatter = null)
        {
            var types = GetSupportedTypes(Configuration).ToList();
            if (
                types.All(type => string.Compare(type.Name, typeName, StringComparison.InvariantCultureIgnoreCase) != 0))
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var executor = new DictionaryDynamicExecutor(typeName, types);
            if (dictionaryItem != null)
            {
                parameters = new[] {executor.DesserializeInstance(dictionaryItem.ToString())};
            }
            var result = executor.Execute(Configuration.DependencyResolver, methodName, parameters);
            if (executor.IsResultEmpty(result))
                return new HttpResponseMessage(HttpStatusCode.NotFound);


            var negotiator = Configuration.Services.GetContentNegotiator();

            var mediaType = "application/json";
            if (formatter == null)
            {
                var negotiationResult = negotiator.Negotiate(
                    result.GetType(), Request, Configuration.Formatters);

                if (negotiationResult == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
                    throw new HttpResponseException(response);
                }

                formatter = negotiationResult.Formatter;
                mediaType = negotiationResult.MediaType.MediaType;
            }
            else if (formatter.SupportedMediaTypes.Count > 0)
            {
                mediaType = formatter.SupportedMediaTypes[0].MediaType;
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent(result.GetType(), result, formatter, mediaType)
            };
        }

        [Route("types")]
        [HttpGet]
        public IHttpActionResult GetTypes()
        {
            var list = GetSupportedTypes(Configuration);

            return Ok(list.Select(t => t.Name));
        }

        private IEnumerable<Type> GetSupportedTypes(HttpConfiguration configuration)
        {
            var service =
                (ISupportedDataEntities) configuration.DependencyResolver.GetService(typeof(ISupportedDataEntities));
            var list = service.GetSupportedDataEntities();
            return list;
        }

        [Route("{typeName:alpha}/list")]
        [HttpGet]
        public HttpResponseMessage GetList(string typeName)

        {
            return DynamicExecute(typeName, "GetList", new object[] {null, null, null});
        }


        [Route("{typeName:alpha}/list/csv")]
        [HttpGet]
        public HttpResponseMessage GetCsvList(string typeName)

        {
            var formatter = Configuration.Formatters.FirstOrDefault(item => item.GetType() == typeof(CsvFormater));
            return DynamicExecute(typeName, "GetList", new object[] {null, null, null}, null, formatter);
        }

        [Route("{typeName:alpha}/list/{pageIndex:int}")]
        [HttpGet]
        public HttpResponseMessage GetList(string typeName, int pageIndex)
        {
            return DynamicExecute(typeName, "GetList", new object[] {pageIndex, null, null});
        }

        [Route("{typeName:alpha}/list/{pageIndex:int}/{pageSize:int}")]
        [HttpGet]
        public HttpResponseMessage GetList(string typeName, int pageIndex, int pageSize)
        {
            return DynamicExecute(typeName, "GetList", new object[] {pageIndex, pageSize, null});
        }

        [Route("{typeName:alpha}/list/{pageIndex:int}/{pageSize:int}/{sortName}")]
        [HttpGet]
        public HttpResponseMessage GetList(string typeName, int? pageIndex, int? pageSize = null,
            string sortName = null)
        {
            return DynamicExecute(typeName, "GetList", new object[] {pageIndex, pageSize, sortName});
        }

        [Route("{typeName:alpha}/list/count")]
        [HttpGet]
        public HttpResponseMessage GetCount(string typeName)
        {
            return DynamicExecute(typeName, "GetCount", null);
        }


        [Route("{typeName:alpha}/{id:int}")]
        [HttpGet]
        [ObjectDoesNotExistFilter]
        public HttpResponseMessage GetItem(string typeName, int id)
        {
            return DynamicExecute(typeName, "GetItem", new object[] {id});
        }

        [Route("{typeName:alpha}")]
        [HttpPost]
        [ObjectAlreadyExistsFilter]
        public HttpResponseMessage Post(string typeName, [FromBody] object item)
        {
            return DynamicExecute(typeName, "CreateItem", null, item);
        }


        [Route("{typeName:alpha}/{id:int}")]
        [HttpPut]
        [ObjectDoesNotExistFilter]
        public HttpResponseMessage Put(string typeName, int id, [FromBody] object item)
        {
            return DynamicExecute(typeName, "UpdateItem", null, item);
        }

        [Route("{typeName:alpha}/{id:int}")]
        [HttpDelete]
        [ObjectDoesNotExistFilter]
        public HttpResponseMessage Delete(string typeName, int id)
        {
            return DynamicExecute(typeName, "DeleteItem", new object[] {id});
        }
    }
}