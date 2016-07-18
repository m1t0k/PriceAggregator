using System;
using System.Linq;
using System.Web.Http;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.DataAccess;
using PriceAggregator.Core.DataAccess.Interfaces;
using PriceAggregator.Core.DataEntity;
using PriceAggregator.Core.DataEntity.Base;
using PriceAggregator.Core.DictionaryProvider;
using PriceAggregator.Core.DictionaryProvider.Interfaces;
using PriceAggregator.Core.Logging;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace PriceAggregator.Core.Ioc
{
    public class SimpleInjectorIoC
    {
        public static void Configure()
        {
            var container = new Container();
            var scope = new WebApiRequestLifestyle();
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

            container.Register(typeof(IGenericDataAccessProvider<>), typeof(MsSqlDataAccessProvider<>), scope);
            container.Register(
                () =>
                    new Lazy<IGenericDataAccessProvider<Category>>(
                        container.GetInstance<IGenericDataAccessProvider<Category>>), scope);

            container.Register(
                RedisClientFactory.CreateRedisStackExchangeInstance<Category>, scope);
            container.Register(typeof(IDictionaryProvider<>), typeof(DictionaryProvider<>), scope);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            //container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}