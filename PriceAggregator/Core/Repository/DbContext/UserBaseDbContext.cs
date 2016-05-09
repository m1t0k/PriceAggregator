using PriceAggregator.Core.Libraries.Logging;
using PriceAggregator.Core.Repository.DbContext.Base;

namespace PriceAggregator.Core.Repository.DbContext
{
    public class UserBaseDbContext : Base.BaseDbContext
    {
        public UserBaseDbContext(ILoggingService logger) : base(logger)
        {
        }
    }
}