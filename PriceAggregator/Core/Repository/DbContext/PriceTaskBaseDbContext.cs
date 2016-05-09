using PriceAggregator.Core.Libraries.Logging;
using PriceAggregator.Core.Repository.DbContext.Base;

namespace PriceAggregator.Core.Repository.DbContext
{
    public class PriceTaskBaseDbContext : Base.BaseDbContext
    {
        public PriceTaskBaseDbContext(ILoggingService logger) : base(logger)
        {
        }
    }
}