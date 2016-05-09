using System;
using System.Collections.Generic;
using System.Linq;
using PriceAggregator.Core.Caching;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Repository.DbContext.Base;

namespace PriceAggregator.Core.Repository.Base
{
    public abstract class GenericRepository<T> : BaseRepository where T : BaseEntity
    {
        private readonly Lazy<IGenericDbContext> _dbContext;

        protected GenericRepository(Lazy<IGenericDbContext> dbContext, Lazy<ICacheRepository> cacheRepository)
            : base(null, cacheRepository)
        {
            _dbContext = dbContext;
        }

        protected new IGenericDbContext GenericDbContext
        {
            get { return _dbContext.Value; }
        }

        private List<T> ReadAllDataFromDb()
        {
            return GenericDbContext.GetList<T>(0, null, null, null);
        }

        public void PopulateCache()
        {
            CacheRepository.PopulateCache(ReadAllDataFromDb);
        }

        protected List<T> GetList(int id, int? pageIndex, int? pageSize, string sortName)
        {
            if (CacheRepository == null)
                return GenericDbContext.GetList<T>(id, pageIndex, pageSize, sortName);
            List<T> list = CacheRepository.GetAll(ReadAllDataFromDb);

            return GenericDbContext.GetList(list.AsQueryable(), id, pageIndex, pageSize, sortName);
        }


        public T GetItem(int id)
        {
            return CacheRepository == null
                ? GenericDbContext.GetItem<T>(id)
                : CacheRepository.GetItem<T>(id, GenericDbContext.GetItem<T>);
        }

        public void CreateItem(int id, T item)
        {
            GenericDbContext.CreateItem(id, item);
            if (CacheRepository != null)
            {
                CacheRepository.AddItem(item);
            }
        }


        public void UpdateItem(int id, T item)
        {
            GenericDbContext.UpdateItem(id, item);
            if (CacheRepository != null)
            {
                CacheRepository.AddItem(item);
            }
        }


        public void DeleteItem(int id)
        {
            GenericDbContext.DeleteItem<T>(id);
            if (CacheRepository != null)
            {
                CacheRepository.DeleteItem<T>(id);
            }
        }
    }
}