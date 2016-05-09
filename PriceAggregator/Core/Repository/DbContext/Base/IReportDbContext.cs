using System.Collections.Generic;
using PriceAggregator.Core.Entities;

namespace PriceAggregator.Core.Repository.DbContext.Base
{
    public interface IReportDbContext : IBaseDbContext
    {
        List<RrpSituationReport> GetRrpSituationReport(int categoryId);
        List<RrpViolationReport> GetRrpViolationReport(int categoryId);
    }
}