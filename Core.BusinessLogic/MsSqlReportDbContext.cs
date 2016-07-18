using System.Collections.Generic;
using System.Data.SqlClient;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.DataAccess.DbContext.Base;
using PriceAggregator.Core.DataEntity;
using PriceAggregator.Core.Libraries.Logging;

namespace PriceAggregator.Core.Repository.DbContext
{
    public class MsSqlReportDbContext : MsSqlReportDbContext, IReportDbContext
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