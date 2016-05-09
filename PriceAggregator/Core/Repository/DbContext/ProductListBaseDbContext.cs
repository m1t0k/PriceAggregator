using PriceAggregator.Core.Libraries.Logging;
using PriceAggregator.Core.Repository.DbContext.Base;

namespace PriceAggregator.Core.Repository.DbContext
{
    public class ProductListBaseDbContext : Base.BaseDbContext
    {
        public ProductListBaseDbContext(ILoggingService logger) : base(logger)
        {
        }
    }
}