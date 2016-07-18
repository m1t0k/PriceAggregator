using System;
using CacheManager.Core;
using PriceAggregator.Core.DataEntity.Base;

namespace Web.Dictionary.Ioc
{
    public class RedisClientFactory<T> where T : BaseEntity
    {
        public Lazy<ICacheManager<T>> CreateRedisInstance()
        {
            var cacheConfig =
                ConfigurationBuilder.BuildConfiguration(
                    settings => { settings.WithRedisCacheHandle("redisConnection", true); });
            return new Lazy<ICacheManager<T>>(()=>CacheFactory.FromConfiguration<T>(cacheConfig));
        }
    }
}