using System;
using PriceAggregator.Core.Cache.Interface;
using PriceAggregator.Core.Caching;
using PriceAggregator.Core.DataAccess.DbContext.Base;
using PriceAggregator.Core.Interfaces;

namespace PriceAggregator.Core.Repository.Base
{
    public abstract class BaseRepository : IDisposable
    {
        private readonly Lazy<IDataCacheProvider<>> _cacheRepository;

        private readonly Lazy<IBaseDbContext> _dbContext;

        protected BaseRepository(Lazy<IBaseDbContext> dbContext, Lazy<IDataCacheProvider<>> cacheRepository)
        {
            _dbContext = dbContext;
            _cacheRepository = cacheRepository;
        }

        protected IDataCacheProvider<> DataCacheProvider
        {
            get { return _cacheRepository.Value; }
        }

        protected IBaseDbContext DbContext
        {
            get { return _dbContext.Value; }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected void Dispose(bool disposing)
        {
        }
    }
}