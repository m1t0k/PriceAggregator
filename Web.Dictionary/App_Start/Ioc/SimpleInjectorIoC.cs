using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CacheManager.Core;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.DataAccess;
using PriceAggregator.Core.DataAccess.Interfaces;
using PriceAggregator.Core.DataEntity;
using PriceAggregator.Core.DataEntity.Base;
using PriceAggregator.Core.DataEntity.Core;
using PriceAggregator.Core.DictionaryProvider;
using PriceAggregator.Core.DictionaryProvider.Interfaces;
using PriceAggregator.Core.Logging;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using Web.Dictionary.Controllers.Base;
using Web.Dictionary.Models;

namespace Web.Dictionary.Ioc
{
    public class SimpleInjectorIoC
    {
        /// var lazyCacheManagerType = (typeof(Lazy
        /// <
        /// >
        /// )) cacheManagerTemplate.MakeGenericType(type.Type);
        /// container.Register(() => new Lazy
        /// <ILoggingService>(container.GetInstance<ILoggingService>), scope);
        /*            
        Lazy<T> LazyWrapper<T>(Container container) where T:class
        {
            return new Lazy<T>(container.GetInstance<T>());
        }
        */
        public static List<Type> TypeList { get; private set; }

        public static void Configure()
        {
            var container = new Container();
            var scope = new WebApiRequestLifestyle();
            container.Options.DefaultScopedLifestyle = scope;

            var dataEntities = GetDataEntities();
            var lazyType = typeof(Lazy<>);

            foreach (var type in dataEntities)
            {
                container.Register(type);

                var iDbTemplate = typeof(IGenericDataAccessProvider<>);
                var iDbType = iDbTemplate.MakeGenericType(type);
                var dbProviderTemplate = typeof(MsSqlDataAccessProvider<>);
                var dbProviderType = dbProviderTemplate.MakeGenericType(type);
                container.Register(iDbType, dbProviderType, scope);

                var cacheManagerTemplate = typeof(ICacheManager<>);
                var cacheManagerType = cacheManagerTemplate.MakeGenericType(type);
                var lazyCacheType = lazyType.MakeGenericType(cacheManagerType);

                var redisFactoryTemplate = typeof(RedisClientFactory<>);
                var redisFactoryType = redisFactoryTemplate.MakeGenericType(type);
                var factoryMethod = redisFactoryType.GetMethod("CreateRedisInstance");

                container.Register(lazyCacheType,
                    () => factoryMethod.Invoke(container.GetInstance(redisFactoryType), null), Lifestyle.Singleton);
            }
           
            container.Register<IDynamicExecutorHelper, DynamicExecutorHelper>(scope);
            container.Register<ISupportedDataEntities, SupportedDataEntities>(Lifestyle.Singleton);
            container.Register<ILoggingService, NLogLoggingService>(Lifestyle.Singleton);
            container.Register(() => new Lazy<ILoggingService>(container.GetInstance<ILoggingService>), scope);
            container.Register(typeof(IDictionaryProvider<>), typeof(DictionaryProvider<>), scope);
            container.Register<ApplicationUser>(scope);
            container.Register<ApplicationDbContext>(scope);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }

        public static List<Type> GetDataEntities()
        {
            var entityAssembly = typeof(Category).Assembly;
            var entityTypes =
                (from type in entityAssembly.GetExportedTypes()
                    where
                        type.Namespace == typeof(Category).Namespace && type.IsClass &&
                        type.BaseType == typeof(BaseEntity)
                    select type).ToList();

            return entityTypes;
        }
    }
}