using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.DataEntity.Base;
using PriceAggregator.Core.DictionaryProvider.Interfaces;
using RestSharp;

namespace Web.BusinessLogic
{
    public class DictionaryRestClient<T> : IDictionaryProvider<T> where T : BaseEntity
    {
        private readonly string _baseApiUrl;
        private readonly ILoggingService _logger;

        public DictionaryRestClient(string baseUrl, ILoggingService logger)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl));

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));


            _logger = logger;
            _baseApiUrl = $"{baseUrl}/api/{typeof(T).Name.ToLower()}/";
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public T GetItem(int id)
        {
            try
            {
                var client = GetRestClientInstance();

                var request = new RestRequest(Method.GET)
                {
                    Resource = $"/{id}"
                };

                var response = client.Execute(request);

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(response.ErrorMessage);
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public void CreateItem(T item)
        {
            try
            {
                var client = GetRestClientInstance();

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

        public void UpdateItem(T item)
        {
            try
            {
                var client = GetRestClientInstance();

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

        public void DeleteItem(int id)
        {
            try
            {
                var client = GetRestClientInstance();

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

        public IEnumerable<T> GetList(int? pageIndex, int? pageSize, string sortExpression)
        {
            try
            {
                var client = GetRestClientInstance();

                var request = new RestRequest(Method.GET)
                {
                    Resource = $"/list/{pageIndex ?? 1}/{pageSize ?? 20}/{sortExpression}"
                };

                var response = client.Execute(request);

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(response.ErrorMessage);
                return JsonConvert.DeserializeObject<List<T>>(response.Content);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }

        public Task<IEnumerable<T>> GetListAsync(int? pageIndex, int? pageSize, string sortExpression)
        {
            var client = GetRestClientInstance();

            var request = new RestRequest(Method.GET)
            {
                Resource = $"/list/{pageIndex ?? 1}/{pageSize ?? 20}/{sortExpression}"
            };
            var tcs = new TaskCompletionSource<IEnumerable<T>>();

            client.ExecuteAsync<List<T>>(request, response =>
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    tcs.SetException(response.ErrorException);
                else
                    tcs.SetResult(JsonConvert.DeserializeObject<List<T>>(response.Content));
            });

            return tcs.Task;
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        private RestClient GetRestClientInstance()
        {
            var client = new RestClient {BaseUrl = new Uri(_baseApiUrl)};
            return client;
        }
    }
}