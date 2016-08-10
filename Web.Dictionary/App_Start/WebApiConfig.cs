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
using Web.Dictionary.Ioc;

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

            /*ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Category>("CategorySet");
            builder.EntitySet<Brand>("BrandSet");
            */

            
            //var dataEntities = SimpleInjectorIoC.GetDataEntities();

            //var route = config.Routes.Where(r => r is ODataRoute).First();
            //var odataRoute = route as ODataRoute;

            var builder = new ODataConventionModelBuilder();
            // var baseMethod = builder.GetType().GetMethod("EntitySet");
            //builder.EntitySet<Brand>("BrandSet");
            // builder.EntitySet<Brand>("BrandSet");

            builder.EntitySet<BaseEntity>("EntitySet");

            //builder.EntitySet<Brand>("EntitySet");
             builder.Function("GetEntitySet").ReturnsCollection<BaseEntity>()
            //.ReturnsCollectionFromEntitySet<BaseEntity>("EntitySet")
            .Parameter<string>("typeName");

            //builder.EntitySet<Category>("CategorySet");
            //builder.EntitySet<BaseEntity>("GetEntitySet");
           
            /*var types =new[] {"" };
            foreach (var dataEntity in dataEntities)
            {
                //if (!dataEntity.IsDefined(typeof (KeyAttribute), true))
                //    continue;
                if(Array.FindIndex(types,item=>dataEntity.Name.Equals(item))<0)
                    continue;
                
                var genericMethod = baseMethod.MakeGenericMethod(dataEntity);
                genericMethod.Invoke(builder, new[] {$"{dataEntity.Name}Set"});
                //builder.EnableLowerCamelCase();
                //config.MapODataServiceRoute($"{dataEntity.Name.ToLower()}", "{dataEntity.Name.ToLower()}",
                //    builder.GetEdmModel());
            }*/

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "api/odata",
                model: builder.GetEdmModel());

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            ///config.SuppressDefaultHostAuthentication();
            ///config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes

            // config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
                );
        }
    }
}