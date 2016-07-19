using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CacheManager.Core;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.DataAccess.Interfaces;
using PriceAggregator.Core.DataEntity.Base;
using PriceAggregator.Core.DictionaryProvider.Interfaces;

namespace PriceAggregator.Core.DictionaryProvider
{
    public class DictionaryProvider<T> : IDictionaryProvider<T> where T : BaseEntity
    {
        protected readonly Lazy<ICacheManager<T>> CacheManager;
        protected readonly IGenericDataAccessProvider<T> DbContext;
        protected readonly Lazy<ILoggingService> Logger;

        public DictionaryProvider(Lazy<ICacheManager<T>> cacheManager,
            IGenericDataAccessProvider<T> dbContext,
            Lazy<ILoggingService> logger)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            DbContext = dbContext;
            CacheManager = cacheManager;
            Logger = logger;
        }

        public T GetItem(int id)
        {
            try
            {
                T item = null;
                var cacheKey = GenerateKey(id);
                if (CacheManager.IsValueCreated)
                    item = CacheManager.Value.Get(cacheKey);

                if (item == null)
                    item = DbContext.GetItem(id);

                if (CacheManager.IsValueCreated && item != null)
                    CacheManager.Value.AddOrUpdate(cacheKey, item, v => item);

                return item;
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        public bool CreateItem(T item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                var result=DbContext.CreateItem(item)>0;
                if (result && CacheManager.IsValueCreated)
                    CacheManager.Value.Add(GenerateKey(item.Id), item);
                return result;
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        public bool UpdateItem(T item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                var result=DbContext.UpdateItem(item)>0;
                
                    if (result && CacheManager.IsValueCreated)
                        CacheManager.Value.AddOrUpdate(GenerateKey(item.Id), item, v => item);
                
                return result;
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        public bool DeleteItem(int id)
        {
            try
            {
                var result=DbContext.DeleteItem(id)>0;
                if (result && CacheManager.IsValueCreated)
                    CacheManager.Value.Remove(GenerateKey(id));
                return result;
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }


        public async Task<IEnumerable<T>> GetListAsync(int? pageIndex, int? pageSize, string sortExpression)
        {
            try
            {
                var sortOptions = SplitSortExpression(sortExpression);
                return await DbContext.GetListAsync(pageIndex, pageSize, sortOptions.Item1, sortOptions.Item2);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        public IEnumerable<T> GetList(int? pageIndex, int? pageSize, string sortExpression)
        {
            try
            {
                var sortOptions = SplitSortExpression(sortExpression);
                return DbContext.GetList(pageIndex, pageSize, sortOptions.Item1, sortOptions.Item2);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        public async Task<int> GetCountAsync()
        {
            try
            {
                return await DbContext.GetCountAsync();
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                DbContext.Dispose();
                if (Logger.IsValueCreated)
                    Logger.Value.Dispose();
            }
            catch (Exception e)
            {
            }
        }

        public int GetCount()
        {
            try
            {
                return DbContext.GetCount();
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }


        private static Tuple<string, bool> SplitSortExpression(string sortExpression)
        {
            var sortName = "";
            var acsending = true;

            if (!string.IsNullOrEmpty(sortExpression))
            {
                var items = sortExpression.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                if (items.Length >= 0)
                {
                    sortName = items[0];
                }
                if (items.Length > 1 &&
                    string.Compare(items[1], "desc", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    acsending = false;
                }
            }

            return Tuple.Create(sortName, acsending);
        }

        protected void LogError(Exception exception)
        {
            Logger.Value?.Error(exception);
        }

        protected virtual string GenerateKey(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return GenerateKey(item.Id);
        }

        protected virtual string GenerateKey(int itemId)
        {
            return $"{typeof(T).FullName}:{itemId}";
        }
    }
}