using System.Configuration;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Owin.Security.OAuth;
using Web.Api.Common.Core.ExceptionHandling;
using Web.Api.Common.Core.Formatters;
using Web.Api.Common.Core.Handler;

namespace Web.Dictionary
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.UseDataContractJsonSerializer = false;
            var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            xml.UseXmlSerializer = false;

            config.Formatters.Clear();
            config.Formatters.Add(json);
            config.Formatters.Add(new CsvFormater());

            var bson = new BsonMediaTypeFormatter();
            bson.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.contoso"));
            config.Formatters.Add(bson);

            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExecutionErrorHandler());
            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.MessageHandlers.Add(new LoggingHandler());
            config.MessageHandlers.Add(new ApiKeyHandler());

            
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            ///config.SuppressDefaultHostAuthentication();
            ///config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
                );
        }
    }
}