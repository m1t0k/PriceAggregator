using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Web.Api.Common.Core.Filters
{
    public class ApiKeyAuthenticationFilter : IAuthenticationFilter
    {
        private const string ApiHeader = "ApiKey";
        private static string ApiKey => ConfigurationManager.AppSettings["ApiKey"];

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (!ValidateDictionaryKey(context.Request))
            {
                context.ErrorResult = context.ErrorResult
                    = new UnauthorizedResult(new[]
                    {
                        new AuthenticationHeaderValue("ApiKey")
                    }, context.Request);
            }
            else
            {
                context.Principal = new GenericPrincipal(new GenericIdentity("auth"), new[] { "user"});
            }

            return Task.FromResult<object>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context,
            CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }

        public bool AllowMultiple => false;

        private bool ValidateDictionaryKey(HttpRequestMessage request)
        {
            IEnumerable<string> values = null;
            if (!request.Headers.TryGetValues(ApiHeader, out values))
            {
                return false;
            }

            var headerValue = values.FirstOrDefault();

            return !string.IsNullOrWhiteSpace(ApiKey) &&
                   string.Compare(ApiKey, headerValue, StringComparison.InvariantCulture) == 0;
        }
    }
}