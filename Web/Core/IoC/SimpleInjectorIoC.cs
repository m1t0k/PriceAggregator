using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.DataAccess;
using PriceAggregator.Core.DataAccess.Interfaces;
using PriceAggregator.Core.DataEntity;
using PriceAggregator.Core.DataEntity.Base;
using PriceAggregator.Core.Logging;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace PriceAggregator.Web.Core.IoC
{
    public class SimpleInjectorIoC
    {
        public static void Configure()
        {
            var container = new Container();
            var scope = new WebRequestLifestyle();
            container.Options.DefaultScopedLifestyle = scope;

            // register models
            var entityAssembly = typeof(Category).Assembly;
            var entityRegistrations =
                from type in entityAssembly.GetExportedTypes()
                where
                    type.Namespace == typeof(Category).Namespace && type.IsClass && type.BaseType == typeof(BaseEntity)
                select new {Type = type};

            foreach (var reg in entityRegistrations)
            {
                container.Register(reg.Type);
            }

            container.Register<ILoggingService, NLogLoggingService>(scope);
            container.Register(() => new Lazy<ILoggingService>(container.GetInstance<ILoggingService>), scope);
         
            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}