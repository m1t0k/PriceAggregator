using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using PriceAggregator.Core.DataEntity;
using PriceAggregator.Core.DataEntity.Base;
using Web.Api.Common.Core.ExceptionHandling;
using Web.Api.Common.Core.Filters;
using Web.Api.Common.Core.Formatters;
using Web.Api.Common.Core.Handler;
using Web.Dictionary.Controllers;
using Web.Dictionary.Ioc;
using Web.Dictionary.Models;

namespace Web.Dictionary
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
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

            config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new ApiKeyAuthenticationFilter());
            config.Filters.Add(new ValidateModelFilter());

            config.Services.Replace(typeof (IExceptionHandler), new GlobalExecutionErrorHandler());
            config.Services.Add(typeof (IExceptionLogger), new GlobalExceptionLogger());
            config.MessageHandlers.Add(new LoggingHandler());


            // register oData
            // New code:
            config.MapHttpAttributeRoutes();

            
            var builder = new ODataConventionModelBuilder();
      
            var function= builder.Function("GetEntitySet").ReturnsCollection<BaseEntity>();
            function.Parameter(typeof(string), "typeName");
            builder.EntitySet<DictionaryType>("Types");

            function= builder.Function("GetEntitySet").ReturnsCollection<BaseEntity>();
            function.Parameter(typeof(string), "typeName");
            function.Parameter(typeof(int), "id");

            builder.EntitySet<DictionaryType>("Types");
            
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "api/odata",
                model: builder.GetEdmModel());

            
            // config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
                );
        }
    }
}