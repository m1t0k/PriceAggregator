using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Web.BusinessLogic.Helpers;
using PriceAggregator.Web.Common;
using RestSharp;

namespace PriceAggregator.Web.Controllers
{
    [Authorize]
    [RoutePrefix("Dictionary")]
    public class DictionaryController : Controller
    {
        private readonly IDictionaryRestClient _dictionaryRestClient;
        private ILoggingService _logger;

        public DictionaryController(IDictionaryRestClient dictionaryRestClient, ILoggingService logger)
        {
            if (dictionaryRestClient == null)
                throw new ArgumentNullException(nameof(dictionaryRestClient));

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _dictionaryRestClient = dictionaryRestClient;
            _logger = logger;
        }

        private ActionResult ParseGetResponse(IRestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new ContentResult
                    {
                        Content = response.Content,
                        ContentEncoding =
                            !string.IsNullOrWhiteSpace(response.ContentEncoding)
                                ? Encoding.GetEncoding(response.ContentEncoding)
                                : null,
                        ContentType = response.ContentType
                    };
                case HttpStatusCode.NotFound:
                    return HttpNotFound();
            }

            return new HttpStatusCodeResult(response.StatusCode, response.ErrorException?.ToString() ?? response.Content);
        }

        private ActionResult ParseActionResponse(IRestResponse response)
        {
            return new HttpStatusCodeResult(response.StatusCode, response.ErrorException?.ToString() ?? response.Content);
        }

        [Route("types")]
        [HttpGet]
        public async Task<ActionResult> GetTypes()
        {
            var result = await _dictionaryRestClient.GetTypesAsync();
            return ParseGetResponse(result);
        }

        [Route("{typeName:alpha}/list")]
        [HttpGet]
        public async Task<ActionResult> GetList(string typeName)
        {
            var result = await _dictionaryRestClient.GetListAsync(typeName, null, null, null);
            return ParseGetResponse(result);
        }


        [Route("{typeName:alpha}/list/csv")]
        [HttpGet]
        public Task<ContentResult> GetCsvList(string typeName)

        {
            return null;
        }

        [Route("{typeName:alpha}/list/{pageIndex:int}")]
        [HttpGet]
        public async Task<ActionResult> GetList(string typeName, int pageIndex)
        {
            var result = await _dictionaryRestClient.GetListAsync(typeName, pageIndex, null, null);
            return ParseGetResponse(result);
        }

        [Route("{typeName:alpha}/list/{pageIndex:int}/{pageSize:int}")]
        [HttpGet]
        public async Task<ActionResult> GetList(string typeName, int pageIndex, int pageSize)
        {
            var result = await _dictionaryRestClient.GetListAsync(typeName, pageIndex, pageSize, null);
            return ParseGetResponse(result);
        }

        [Route("{typeName:alpha}/list/{pageIndex:int}/{pageSize:int}/{sortName}")]
        [HttpGet]
        public async Task<ActionResult> GetList(string typeName, int? pageIndex, int? pageSize = null,
            string sortName = null)
        {
            var result = await _dictionaryRestClient.GetListAsync(typeName, pageIndex, pageSize, sortName);
            return ParseGetResponse(result);
        }

        [Route("{typeName:alpha}/list/count")]
        [HttpGet]
        public async Task<ActionResult> GetCount(string typeName)
        {
            var result = await _dictionaryRestClient.GetCountAsync(typeName);
            return ParseGetResponse(result);
        }

        [Route("{typeName:alpha}/{id:int}")]
        [HttpGet]
        public async Task<ActionResult> GetItem(string typeName, int id)
        {
            var result = await _dictionaryRestClient.GetItemAsync(typeName, id);
            return ParseGetResponse(result);
        }

        [Route("{typeName:alpha}")]
        [HttpPost]
        [ModelStateValidatorFilter]
        public async Task<ActionResult> Post(string typeName, [DynamicJson] dynamic item)
        {
           
            var result = await _dictionaryRestClient.CreateItemAsync(typeName, item);
            return ParseActionResponse(result);
        }


        [Route("{typeName:alpha}/{id:int}")]
        [HttpPut]
        [ModelStateValidatorFilter]
        public async Task<ActionResult> Put(string typeName, int id, [DynamicJson] dynamic item)
        {
            
            var result = await _dictionaryRestClient.UpdateItemAsync(typeName, id, item);
            return ParseActionResponse(result);
        }

        [Route("{typeName:alpha}/{id:int}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(string typeName, int id)
        {
            var result = await _dictionaryRestClient.DeleteItemAsync(typeName, id);
            return ParseActionResponse(result);
        }
    }
}