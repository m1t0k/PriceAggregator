using PriceAggregator.Core.Libraries.Logging;
using PriceAggregator.Core.Repository.DbContext.Base;

namespace PriceAggregator.Core.Repository.DbContext
{
    public class MsSqlDbContext : GenericDbContext
    {
        public MsSqlDbContext(ILoggingService logger) : base(logger)
        {
        }
    }
}