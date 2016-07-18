using System.Collections.Generic;
using PriceAggregator.Core.DataEntity;

namespace PriceAggregator.Core.DataAccess.DbContext.Base
{
    public interface IReportDbContext : IGenericDbContext
    {
        IEnumerable<RrpSituationReport> GetRrpSituationReport(int categoryId);
        IEnumerable<RrpViolationReport> GetRrpViolationReport(int categoryId);
    }
}