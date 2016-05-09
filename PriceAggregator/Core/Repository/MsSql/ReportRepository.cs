using System;
using System.Collections.Generic;
using PriceAggregator.Core.Caching;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Repository.Base;
using PriceAggregator.Core.Repository.DbContext.Base;
using PriceAggregator.Core.Repository.Interfaces;

namespace PriceAggregator.Core.Repository.MsSql
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        private readonly Lazy<IReportDbContext> _dbContext;

        public ReportRepository(Lazy<IReportDbContext> dbContext, Lazy<ICacheRepository> cacheRepository)
            : base(null, cacheRepository)
        {
            _dbContext = dbContext;
        }

        protected new IReportDbContext DbContext
        {
            get { return _dbContext.Value; }
        }


        public List<RrpSituationReport> GetRrpSituationReport(int categoryId)
        {
            return CacheRepository == null
                ? DbContext.GetRrpSituationReport(categoryId)
                : CacheRepository.GetAll(() => DbContext.GetRrpSituationReport(categoryId));
        }

        public List<RrpViolationReport> GetRrpViolationReport(int categoryId)
        {
            return CacheRepository == null
                ? DbContext.GetRrpViolationReport(categoryId)
                : CacheRepository.GetAll(() => DbContext.GetRrpViolationReport(categoryId));
        }
    }
}