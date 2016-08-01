using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            _baseApiUrl = $"{baseUrl}/api/dictionary/";
            _apiKey = apiKey;
        }

        public T GetItem<T>(int id) where T : BaseEntity
        {
            try
            {
                var client = GetRestClientInstance<T>();

                var request = new RestRequest(Method.GET)
                {
                    Resource = $"{typeof (T).Name.ToLower()}/{id}"
                };

                var response = client.Execute(request);

                return ParseGetResponse<T>(response);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public void CreateItem<T>(T item) where T : BaseEntity
        {
            try
            {
                var client = GetRestClientInstance<T>();

                var request = new RestRequest(Method.PUT);

                var response = client.Execute(request);

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(response.ErrorMessage);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public void UpdateItem<T>(T item) where T : BaseEntity
        {
            try
            {
                var client = GetRestClientInstance<T>();

                var request = new RestRequest(Method.POST);

                var response = client.Execute(request);

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(response.ErrorMessage);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public void DeleteItem<T>(int id) where T : BaseEntity
        {
            try
            {
                var client = GetRestClientInstance<T>();

                var request = new RestRequest(Method.DELETE)
                {
                    Resource = $"/{id}"
                };

                var response = client.Execute(request);

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(response.ErrorMessage);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public IEnumerable<T> GetList<T>(int? pageIndex, int? pageSize, string sortExpression) where T : BaseEntity
        {
            try
            {
                var client = GetRestClientInstance<T>();

                var request = new RestRequest(Method.GET)
                {
                    Resource = $"/list/{pageIndex ?? 1}/{pageSize ?? 20}/{sortExpression}"
                };

                var response = client.Execute(request);

                return ParseGetResponse<List<T>>(response);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public Task<IEnumerable<T>> GetListAsync<T>(int? pageIndex, int? pageSize, string sortExpression)
            where T : BaseEntity
        {
            try
            {
                var client = GetRestClientInstance<T>();

                var request = new RestRequest(Method.GET)
                {
                    Resource = $"/list/{pageIndex ?? 1}/{pageSize ?? 20}/{sortExpression}"
                };
                var tcs = new TaskCompletionSource<IEnumerable<T>>();

                client.ExecuteAsync<List<T>>(request, (response, t) => { ParseGetResponse(response, tcs); });

                return tcs.Task;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public Task<int> GetCountAsync<T>() where T : BaseEntity
        {
            try
            {
                var client = GetRestClientInstance<T>();
                var request = new RestRequest(Method.GET)
                {
                    Resource = "/list/count"
                };
                var tcs = new TaskCompletionSource<int>();

                client.ExecuteAsync<T>(request, (response, t) => { ParseGetResponse(response, tcs); });

                return tcs.Task;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public int GetCount<T>() where T : BaseEntity
        {
            try
            {
                var client = GetRestClientInstance<T>();
                var request = new RestRequest(Method.GET)
                {
                    Resource = "/list/count"
                };
                var response = client.Execute(request);

                return ParseGetResponse<int>(response);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public IEnumerable<string> GetTypes()
        {
            try
            {
                var client = GetRestClientInstance("");

                var request = new RestRequest(Method.GET)
                {
                    Resource = "types"
                };

                var response = client.Execute(request);

                return ParseGetResponse<List<string>>(response);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        private static T ParseGetResponse<T>(IRestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return JsonConvert.DeserializeObject<T>(response.Content);
                case HttpStatusCode.NotFound:
                    return default(T);
            }

            throw new Exception(response.ErrorMessage);
        }

        private static Task<T> ParseGetResponse<T>(IRestResponse response, TaskCompletionSource<T> tcs)
        {
            if (tcs == null)
                throw new ArgumentNullException(nameof(tcs));

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    tcs.SetResult(JsonConvert.DeserializeObject<T>(response.Content));
                    break;
                case HttpStatusCode.NotFound:
                    tcs.SetResult(default(T));
                    break;
                default:
                    tcs.SetException(new Exception(response.ErrorMessage));
                    break;
            }

            return tcs.Task;
        }

        private RestClient GetRestClientInstance<T>() where T : BaseEntity
        {
            return GetRestClientInstance(typeof (T).Name.ToLower());
        }

        private RestClient GetRestClientInstance(string typeName)
        {
            var client = new RestClient {BaseUrl = new Uri($"{_baseApiUrl}{typeName}")};
            client.AddDefaultHeader("ApiKey", _apiKey);
            return client;
        }
    }
}