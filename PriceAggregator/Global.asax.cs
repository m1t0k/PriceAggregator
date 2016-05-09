using System;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PriceAggregator.Core.Ioc;
using PriceAggregator.Core.Repository;
using PriceAggregator.Core.Repository.Initialization;

namespace PriceAggregator
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                Trace.TraceInformation("App is being started..");

                AutofacIoc.Configure();
                DataCache.InitializeOnStartup();

                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);

                Trace.TraceInformation("App is started.");
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                throw;
            }
        }
    }
}