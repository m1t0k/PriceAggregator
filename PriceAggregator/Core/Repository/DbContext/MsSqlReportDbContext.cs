using System.Collections.Generic;
using System.Data.SqlClient;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Libraries.Logging;
using PriceAggregator.Core.Repository.DbContext.Base;

namespace PriceAggregator.Core.Repository.DbContext
{
    public class MsSqlReportDbContext : BaseDbContext, IReportDbContext
    {
        public MsSqlReportDbContext(ILoggingService logger) : base(logger)
        {
        }

        public List<RrpSituationReport> GetRrpSituationReport(int categoryId)
        {
            return
                SqlQuery<RrpSituationReport>(
                    "exec dbo.GetRrpStats @ProductListId=@ProductListId",
                    new SqlParameter {ParameterName = "@ProductListId", Value = categoryId});
        }

        public List<RrpViolationReport> GetRrpViolationReport(int categoryId)
        {
            return SqlQuery<RrpViolationReport>(
                "exec dbo.GetRetailersViolations @ProductListId=@ProductListId",
                new SqlParameter {ParameterName = "@ProductListId", Value = categoryId});
        }
    }
}