using System;
using System.Collections.Generic;
using System.Linq;
using PriceAggregator.Core.Cache.Interface;
using PriceAggregator.Core.Caching;
using PriceAggregator.Core.DataAccess.DbContext.Base;
using PriceAggregator.Core.DataEntity;
using PriceAggregator.Core.DataEntity.Base;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Interfaces;

namespace PriceAggregator.Core.Repository.Base
{
    public abstract class GenericRepository<T> : BaseRepository where T : BaseEntity
    {
        private readonly Lazy<IGenericDbContext> _dbContext;

        protected GenericRepository(Lazy<IGenericDbContext> dbContext, Lazy<IDataCacheProvider<>> cacheRepository)
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
            DataCacheProvider.PopulateCache(ReadAllDataFromDb);
        }

        protected List<T> GetList(int id, int? pageIndex, int? pageSize, string sortName)
        {
            if (DataCacheProvider == null)
                return GenericDbContext.GetList<T>(id, pageIndex, pageSize, sortName);
            List<T> list = DataCacheProvider.GetAll(ReadAllDataFromDb);

            return GenericDbContext.GetList(list.AsQueryable(), id, pageIndex, pageSize, sortName);
        }


        public T GetItem(int id)
        {
            return DataCacheProvider == null
                ? GenericDbContext.GetItem<T>(id)
                : DataCacheProvider.GetItem<T>(id, GenericDbContext.GetItem<T>);
        }

        public void CreateItem(int id, T item)
        {
            GenericDbContext.CreateItem(id, item);
            if (DataCacheProvider != null)
            {
                DataCacheProvider.AddItem(item);
            }
        }


        public void UpdateItem(int id, T item)
        {
            GenericDbContext.UpdateItem(id, item);
            if (DataCacheProvider != null)
            {
                DataCacheProvider.AddItem(item);
            }
        }


        public void DeleteItem(int id)
        {
            GenericDbContext.DeleteItem<T>(id);
            if (DataCacheProvider != null)
            {
                DataCacheProvider.DeleteItem<T>(id);
            }
        }
    }
}