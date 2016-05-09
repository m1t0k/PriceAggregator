using System;
using System.Data.Entity;
using System.Web.DynamicData;

namespace Web.Core.DbContext.Base
{
    public class BaseDbContext<T> : System.Data.Entity.DbContext where T : class
    {
        public DbSet<T> DbSet { get; set; }

        public BaseDbContext(string nameOrConnectionString)
            : base("PriceAggregator")
        {
        }

        public BaseDbContext()
            : base("PriceAggregator")
        {
        }

        public new void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}