using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using PriceAggregator.Core.Caching;
using PriceAggregator.Core.Libraries.Logging;
using PriceAggregator.Core.Repository.DbContext;
using PriceAggregator.Core.Repository.DbContext.Base;
using PriceAggregator.Core.Repository.Interfaces;
using PriceAggregator.Core.Repository.MsSql;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Jil;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace PriceAggregator.Core.Ioc
{
    public class AutofacIoc
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();


            // Get your HttpConfiguration.
            HttpConfiguration config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);


            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof (ConnectionMultiplexer)));
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof (StackExchangeRedisCacheClient)));
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof (NewtonsoftSerializer)));
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof (JilSerializer)));
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof (RedisHost)));

            builder.Register(c => new NLogLoggingService()).As<ILoggingService>().SingleInstance();
            builder.RegisterType(typeof (MsSqlReportDbContext)).As<IReportDbContext>().InstancePerDependency();
            builder.RegisterType(typeof (MsSqlDbContext)).As<IGenericDbContext>().InstancePerDependency();


            builder.RegisterType(typeof (NewtonsoftSerializer)).As<ISerializer>().InstancePerRequest();
            builder.RegisterType(typeof (ConnectionMultiplexer)).InstancePerRequest();
            builder.RegisterType(typeof (StackExchangeRedisCacheClient)).As<ICacheClient>().InstancePerRequest();
            builder.RegisterType(typeof (IDataCache));

            builder.RegisterType(typeof (RedisStackExchangeRepository)).WithParameters(new List<Parameter>
            {
                new NamedParameter("redisClient",
                    new StackExchangeRedisCacheClient(new NewtonsoftSerializer(),
                        RedisStackExchangeRepository.ConnectionString))
            }).As<ICacheRepository>().InstancePerRequest();

            builder.RegisterType(typeof (ReportRepository)).As<IReportRepository>().InstancePerDependency();
            builder.RegisterType(typeof (BrandRepository)).As<IBrandRepository>().InstancePerDependency();
            builder.RegisterType(typeof (CategoryRepository)).As<ICategoryRepository>().InstancePerDependency();
            builder.RegisterType(typeof (RetailerRepository)).As<IRetailerRepository>().InstancePerDependency();
            builder.RegisterType(typeof (AutofacWebApiDependencyResolver)).As(typeof (IDependencyResolver)).SingleInstance();

            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}