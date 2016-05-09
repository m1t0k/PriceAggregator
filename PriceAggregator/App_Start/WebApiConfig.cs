using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using CacheCow.Server;
using Microsoft.Owin.Security.OAuth;
using PriceAggregator.Core.ExceptionHandling;

namespace PriceAggregator
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            JsonMediaTypeFormatter json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.UseDataContractJsonSerializer = false;
            XmlMediaTypeFormatter xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            xml.UseXmlSerializer = false;
            config.Formatters.Clear();
            config.Formatters.Add(json);

            //config.Services.Replace(typeof (IContentNegotiator), new JsonContentNegotiator(new Newtonsoft.Json.JsonSerializer()));

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("pricetaskstatus", typeof (PriceTaskStatusConstraint));
            config.MapHttpAttributeRoutes(constraintResolver);

            config.Services.Replace(typeof (IExceptionHandler), new GlobalExecutionErrorHandler());
            config.Services.Add(typeof (IExceptionLogger), new GlobalExceptionLogger());
            //var cachingHandler = new CachingHandler(new HttpConfiguration(), new InMemoryEntityTagStore());


            //GlobalConfiguration.Configuration.MessageHandlers.Add(cachingHandler);


            // Web API routes
            //config.MapHttpAttributeRoutes();
        }
    }
}