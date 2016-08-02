using System;
using System.Threading.Tasks;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.DataEntity.Base;
using RestSharp;

namespace PriceAggregator.Web.BusinessLogic.Helpers
{
    public class DictionaryRestClient : IDictionaryRestClient
    {
        private readonly string _apiKey;
        private readonly string _baseApiUrl;
        private readonly ILoggingService _logger;

        public DictionaryRestClient(string baseUrl, string apiKey, ILoggingService logger)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl));

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _logger = logger;
            _baseApiUrl = $"{baseUrl}/";
            _apiKey = apiKey;
        }

        public async Task<IRestResponse> GetListAsync(string typeName, int? pageIndex, int? pageSize,
            string sortExpression)
        {
            try
            {
                var request = new RestRequest(Method.GET)
                {
                    Resource = $"/list/{pageIndex ?? 1}/{pageSize ?? 20}/{sortExpression}"
                };
                return await ExecuteAsync(typeName, request);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public async Task<IRestResponse> GetCountAsync(string typeName)
        {
            try
            {
                var request = new RestRequest(Method.GET)
                {
                    Resource = "/list/count"
                };
                return await ExecuteAsync(typeName, request);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public async Task<IRestResponse> GetTypesAsync()
        {
            try
            {
                var request = new RestRequest(Method.GET)
                {
                    Resource = "types"
                };
                return await ExecuteAsync("", request);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public async Task<IRestResponse> CreateItemAsync(string typeName, BaseEntity item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                var request = new RestRequest(Method.POST) {RequestFormat = DataFormat.Json};
                return await ExecuteAsync(typeName, request);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public async Task<IRestResponse> UpdateItemAsync(string typeName, BaseEntity item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                var request = new RestRequest(Method.PUT) {RequestFormat = DataFormat.Json, Resource = $"/{item.Id}"};
                return await ExecuteAsync(typeName, request);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public async Task<IRestResponse> DeleteItemAsync(string typeName, int id)
        {
            try
            {
                var request = new RestRequest(Method.DELETE)
                {
                    Resource = $"/{id}"
                };
                return await ExecuteAsync(typeName, request);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public async Task<IRestResponse> GetItemAsync(string typeName, int id)
        {
            try
            {
                var request = new RestRequest(Method.GET)
                {
                    Resource = $"{id}"
                };
                return await ExecuteAsync(typeName, request);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }


        private Task<IRestResponse> ExecuteAsync(string typeName, IRestRequest request)
        {
            var client = CreateRestClientInstance(typeName);
            var tcs = new TaskCompletionSource<IRestResponse>();
            client.ExecuteAsync(request, (response, t) => { ParseAsyncResponse(response, tcs); });
            return tcs.Task;
        }

        private static void ParseAsyncResponse(IRestResponse response,
            TaskCompletionSource<IRestResponse> tcs)
        {
            if (tcs == null)
                throw new ArgumentNullException(nameof(tcs));

            tcs.SetResult(response);
        }

        private RestClient CreateRestClientInstance(string typeName)
        {
            var client = new RestClient {BaseUrl = new Uri($"{_baseApiUrl}{typeName}")};

            client.AddDefaultHeader("ApiKey", _apiKey);
            return client;
        }
    }
}