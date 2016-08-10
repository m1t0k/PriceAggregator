using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using PriceAggregator.Core.DataEntity.Base;

namespace Web.Dictionary.Controllers.Base
{
    public class DynamicExecutorHelper : IDynamicExecutorHelper
    {
        public IEnumerable<Type> GetSupportedTypes(HttpConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var service =
                (ISupportedDataEntities) configuration.DependencyResolver.GetService(typeof (ISupportedDataEntities));
            var list = service.GetSupportedDataEntities();
            return list;
        }

        public HttpResponseMessage ExecuteWithFormatter(HttpConfiguration configuration, HttpRequestMessage request,
            string typeName, string methodName, object[] parameters,
            object dictionaryItem = null, MediaTypeFormatter formatter = null)
        {
            Type type = null;
            var result = Execute(configuration, request,
                typeName, methodName, parameters, dictionaryItem, out type);

            return FormatResult(type, result, configuration, request, formatter);
        }

        public object Execute(HttpConfiguration configuration, HttpRequestMessage request,
            string typeName, string methodName, object[] parameters,
            object dictionaryItem = null)
        {
            Type type = null;
            return Execute(configuration, request,
                typeName, methodName, parameters, dictionaryItem, out type);
        }

        private object Execute(HttpConfiguration configuration, HttpRequestMessage request,
            string typeName, string methodName, object[] parameters,
            object dictionaryItem, out Type type)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentNullException(nameof(typeName));

            if (string.IsNullOrWhiteSpace(methodName))
                throw new ArgumentNullException(nameof(methodName));
            type = null;
            var executor = CreateDictionaryDynamicExecutor(configuration, typeName);
            if (executor == null)
            {
                throw new ArgumentNullException(nameof(executor));
            }
            if (dictionaryItem != null)
            {
                parameters = new[] {executor.DesserializeInstance(dictionaryItem.ToString())};
            }
            type = executor.Type;
            return executor.Execute(configuration.DependencyResolver, methodName, parameters);
        }


        private HttpResponseMessage FormatResult(Type type, object result,
            HttpConfiguration configuration, HttpRequestMessage request, MediaTypeFormatter formatter)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (DictionaryDynamicExecutor.IsResultEmpty(type, result))
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            var negotiator = configuration.Services.GetContentNegotiator();

            var mediaType = "application/json";
            if (formatter == null)
            {
                var negotiationResult = negotiator.Negotiate(
                    result.GetType(), request, configuration.Formatters);

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

        private DictionaryDynamicExecutor CreateDictionaryDynamicExecutor(HttpConfiguration configuration,
            string typeName)
        {
            var types = GetSupportedTypes(configuration).ToList();
            if (
                types.All(type => string.Compare(type.Name, typeName, StringComparison.InvariantCultureIgnoreCase) != 0))
            {
                return null;
            }

            return new DictionaryDynamicExecutor(typeName, types);
        }
    }
}