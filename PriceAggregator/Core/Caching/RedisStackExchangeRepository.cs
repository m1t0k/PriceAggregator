using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using PriceAggregator.Core.Entities;
using StackExchange.Redis.Extensions.Core;
using WebGrease.Css.Extensions;

namespace PriceAggregator.Core.Caching
{
    public class RedisStackExchangeRepository : ICacheRepository
    {
        private readonly ICacheClient _redisClient;


        public RedisStackExchangeRepository(ICacheClient redisClient)
        {
            _redisClient = redisClient; //new StackExchangeRedisCacheClient(serializer, ConnectionString);
        }


        public static string ConnectionString
        {
            get
            {
                return
                    WebConfigurationManager.AppSettings["redis.connectionString"];
            }
        }

        public static int ExpirationTimeOut
        {
            get
            {
                int timeOut = 0;
                if (Int32.TryParse(
                    WebConfigurationManager.AppSettings["redis.expirationTimeOut"], out timeOut))
                {
                    return timeOut;
                }

                return 10;
            }
        }

        public List<T> GetAll<T>(Func<List<T>> getItemsFromDb)
            where T : BaseEntity
        {
            var items = new List<T>();
            IEnumerable<string> searchKeys = _redisClient.SearchKeys(GetSearchPattern<T>());
            if (!searchKeys.Any())
            {
                PopulateCache(getItemsFromDb);
                searchKeys = _redisClient.SearchKeys(GetSearchPattern<T>());
            }
            items.AddRange(searchKeys.Select(searchKey => _redisClient.Get<T>(searchKey))
                    .Where(item => item != null));
            
            return items;
        }

        public void PopulateCache<T>(Func<List<T>> getItemsFromDb)
            where T : BaseEntity
        {
            AddAll(PrepareDataForCaching(getItemsFromDb()));
        }

        public void DeleteItem<T>(int itemId) where T : BaseEntity
        {
            _redisClient.Remove(GenerateKey<T>(itemId));
        }

        public void AddAll<T>(IList<Tuple<string, T>> items) where T : BaseEntity
        {
            List<string> keys = items.Select(item => item.Item1).ToList();
            _redisClient.RemoveAll(keys);
            items.ForEach(item => _redisClient.Add(item.Item1, item.Item2));
        }


        public string GenerateKey<T>(T item) where T : BaseEntity
        {
            return String.Format("{0}:{1}", typeof (T).FullName, item.Id);
        }

        public string GenerateKey<T>(int itemId) where T : BaseEntity
        {
            return String.Format("{0}:{1}", typeof (T).FullName, itemId);
        }

        public T GetItem<T>(int itemId, Antlr.Runtime.Misc.Func<int, T> getItemFromDb) where T : BaseEntity
        {
            T item = null;
            string redisKey = GenerateKey<T>(itemId);

            item = _redisClient.Get<T>(redisKey);
            if (item != null) return item;

            // make a small delay
            item = getItemFromDb(itemId);
            if (item != null)
                _redisClient.Add(redisKey, item, new TimeSpan(0, 0, ExpirationTimeOut, 0));


            return item;
        }

        public void DeleteAll<T>(List<int> itemIds) where T:BaseEntity
        {
            foreach (int itemId in itemIds)
            {
                _redisClient.Remove(GenerateKey<T>(itemId));
            }
        }

        protected List<Tuple<string, T>> PrepareDataForCaching<T>(List<T> items) where T : BaseEntity
        {
            return
                items.Select(
                    item =>
                        new Tuple<string, T>(
                            GenerateKey(item), item))
                    .ToList();
        }

        public void AddItem<T>(T item) where T : BaseEntity
        {
            if (item != null)
                _redisClient.Add(GenerateKey(item), item, new TimeSpan(0, 0, ExpirationTimeOut, 0));
        }

        private string GetSearchPattern<T>() where T : BaseEntity
        {
            return String.Format("{0}:*", typeof (T).FullName);
        }
    }
}