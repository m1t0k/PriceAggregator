using System;
using PriceAggregator.Core.Caching;
using PriceAggregator.Core.Repository.DbContext.Base;

namespace PriceAggregator.Core.Repository.Base
{
    public abstract class BaseRepository : IDisposable
    {
        private readonly Lazy<ICacheRepository> _cacheRepository;

        private readonly Lazy<IBaseDbContext> _dbContext;

        protected BaseRepository(Lazy<IBaseDbContext> dbContext, Lazy<ICacheRepository> cacheRepository)
        {
            _dbContext = dbContext;
            _cacheRepository = cacheRepository;
        }

        protected ICacheRepository CacheRepository
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