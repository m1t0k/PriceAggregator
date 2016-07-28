using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Api.Common.Core.Handler
{
    public class ApiKeyHandler : DelegatingHandler
    {
        private const string ApiHeader = "ApiKey";
        private static string ApiKey => ConfigurationManager.AppSettings["ApiKey"];

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!ValidateDictionaryKey(request))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }

            return base.SendAsync(request, cancellationToken);
        }

        private bool ValidateDictionaryKey(HttpRequestMessage request)
        {
            IEnumerable<string> values = null;
            if (!request.Headers.TryGetValues(ApiHeader, out values))
            {
                return false;
            }

            var headerValue = values.FirstOrDefault();

            return !string.IsNullOrWhiteSpace(ApiKey) && string.Compare(ApiKey, headerValue, StringComparison.InvariantCulture) == 0;
        }
    }
}