using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.DataEntity;
using PriceAggregator.Core.DataEntity.Base;
using PriceAggregator.Core.Logging;
using PriceAggregator.Web.Controllers;
using PriceAggregator.Web.Models;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace PriceAggregator.Web.IoC
{
    public class SimpleInjectorIoC
    {
        public static void Configure()
        {
            var container = new Container();
            var scope = new WebRequestLifestyle();
            container.Options.DefaultScopedLifestyle = scope;

            // register models
            var entityAssembly = typeof (Category).Assembly;
            var entityRegistrations =
                from type in entityAssembly.GetExportedTypes()
                where
                    type.Namespace == typeof (Category).Namespace && type.IsClass &&
                    type.BaseType == typeof (BaseEntity)
                select new {Type = type};

            foreach (var reg in entityRegistrations)
            {
                container.Register(reg.Type);
            }

            container.Register<ILoggingService, NLogLoggingService>(scope);
            container.Register(() => new Lazy<ILoggingService>(container.GetInstance<ILoggingService>), scope);

            container.RegisterPerWebRequest(() =>
                container.IsVerifying
                    ? new OwinContext(new Dictionary<string, object>()).Authentication
                    : HttpContext.Current.GetOwinContext().Authentication);

            container.Register(
                () => ConfigurationProvider.CreateDictionaryRestClient(container.GetInstance<ILoggingService>()));

            container.Register(typeof (ApplicationDbContext));
            container.Register(typeof (IUserStore<ApplicationUser>),
                () => new UserStore<ApplicationUser>(container.GetInstance<ApplicationDbContext>()), scope);

            // This is an extension method from the integration package.
            container.Register<AngularTemplatesController>(scope);
            container.Register<DashboardController>(scope);
            container.Register<HomeController>(scope);

            container.RegisterMvcIntegratedFilterProvider();

            // todo fix it later
            //container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}