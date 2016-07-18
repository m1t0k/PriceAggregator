using System.Configuration;
using PriceAggregator.Core.Cache;
using PriceAggregator.Core.Cache.Interface;
using PriceAggregator.Core.DataEntity.Base;

namespace PriceAggregator.Core.Ioc
{
    public class RedisClientFactory
    {
        private static readonly string RedisConnectionString =
            ConfigurationManager.AppSettings["Redis.ConnectionString"];

        public static IDataCacheProvider<T> CreateRedisStackExchangeInstance<T>() where T : BaseEntity
        {
            return new RedisStackExchangeProvider<T>(RedisConnectionString);
        }
    }
}