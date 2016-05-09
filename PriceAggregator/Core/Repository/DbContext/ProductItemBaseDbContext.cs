using PriceAggregator.Core.Libraries.Logging;
using PriceAggregator.Core.Repository.DbContext.Base;

namespace PriceAggregator.Core.Repository.DbContext
{
    public class ProductItemBaseDbContext : Base.BaseDbContext
    {
        public ProductItemBaseDbContext(ILoggingService logger) : base(logger)
        {
        }
    }
}