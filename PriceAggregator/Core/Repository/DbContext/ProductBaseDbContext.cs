using PriceAggregator.Core.Libraries.Logging;
using PriceAggregator.Core.Repository.DbContext.Base;

namespace PriceAggregator.Core.Repository.DbContext
{
    public class ProductBaseDbContext : Base.BaseDbContext
    {
        public ProductBaseDbContext(ILoggingService logger) : base(logger)
        {
        }
    }
}