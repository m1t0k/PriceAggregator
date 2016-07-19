using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using PriceAggregator.Core.DataEntity.Base;
using PriceAggregator.Core.DictionaryProvider.Interfaces;
using Web.Api.Common.Core.Filters;
using Web.Dictionary.Controllers.Base;

namespace Web.Dictionary.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/dictionary")]
    public class DictionaryController : BaseController
    {
        private IHttpActionResult DynamicExecute(string typeName, string methodName, object[] parameters,
            object dictionaryItem = null)
        {
            var types = GetSupportedTypes(Configuration);
            if (
                types.All(type => string.Compare(type.Name, typeName, StringComparison.InvariantCultureIgnoreCase) != 0))
            {
                return NotFound();
            }
            
            var executor = new DictionaryDynamicExecutor(typeName, types);
            if (dictionaryItem != null)
            {
                parameters = new[] {executor.DesserializeInstance(dictionaryItem.ToString())};
            }
            var result = executor.Execute(Configuration.DependencyResolver, methodName, parameters);
            if (executor.IsResultEmpty(result))
                return NotFound();
              
            return Ok(result);
        }

        [Route("types")]
        [HttpGet]
        public IHttpActionResult GetTypes()
        {
            var list = GetSupportedTypes(Configuration);

            return Ok(list.Select(t => t.Name));
        }

        private  IEnumerable<Type> GetSupportedTypes(HttpConfiguration configuration)
        {
            var service =
                (ISupportedDataEntities) configuration.DependencyResolver.GetService(typeof(ISupportedDataEntities));
            var list = service.GetSupportedDataEntities();
            return list;
        }

        [Route("{typeName:alpha}/list")]
        [HttpGet]
        public IHttpActionResult GetList(string typeName)
        {
            return DynamicExecute(typeName, "GetList", new object[] {null, null, null});
        }

        [Route("{typeName:alpha}/list/{pageIndex:int}")]
        [HttpGet]
        public IHttpActionResult GetList(string typeName, int pageIndex)
        {
            return DynamicExecute(typeName, "GetList", new object[] {pageIndex, null, null});
        }

        [Route("{typeName:alpha}/list/{pageIndex:int}/{pageSize:int}")]
        [HttpGet]
        public IHttpActionResult GetList(string typeName, int pageIndex, int pageSize)
        {
            return DynamicExecute(typeName, "GetList", new object[] {pageIndex, pageSize, null});
        }

        [Route("{typeName:alpha}/list/{pageIndex:int}/{pageSize:int}/{sortName}")]
        [HttpGet]
        public IHttpActionResult GetList(string typeName, int? pageIndex, int? pageSize = null,
            string sortName = null)
        {
            return DynamicExecute(typeName, "GetList", new object[] {pageIndex, pageSize, sortName});
        }

        [Route("{typeName:alpha}/list/count")]
        [HttpGet]
        public IHttpActionResult GetCount(string typeName)
        {
            return DynamicExecute(typeName, "GetCount", null);
        }


        [Route("{typeName:alpha}/{id:int}")]
        [HttpGet]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult GetItem(string typeName, int id)
        {
            return DynamicExecute(typeName, "GetItem", new object[] {id});
        }

        [Route("{typeName:alpha}")]
        [HttpPost]
        [ObjectAlreadyExistsFilter]
        public IHttpActionResult Post(string typeName, [FromBody] object item)
        {
            return DynamicExecute(typeName, "CreateItem", null, item);
        }

        [Route("{typeName:alpha}/{id:int}")]
        [HttpPut]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Put(string typeName, int id, [FromBody] object item)
        {
            return DynamicExecute(typeName, "UpdateItem", null, item);
        }

        [Route("{typeName:alpha}/{id:int}")]
        [HttpDelete]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Delete(string typeName, int id)
        {
            return DynamicExecute(typeName, "DeleteItem", new object[] {id});
        }
    }
}