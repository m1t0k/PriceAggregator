using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using PriceAggergator.Core.Logging.Inteface;

namespace Web.Api.Common.Core.Handler
{
    public class LoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var logger =
                   request.GetDependencyScope().GetService(typeof(ILoggingService)) as ILoggingService;

            await Task.Run(() =>
            {
                logger?.Debug(
                    $"{request.RequestUri}\t{request.Method}\t{request.Headers}\t{request.Content.ReadAsStringAsync().Result}");
            });

            var response = base.SendAsync(request, cancellationToken);


            await Task.Run(() =>
            {
                logger?.Debug(
                    $"request.RequestUri\tresponse.Result.StatusCode");
            });

            return await response;
        }
    }
}