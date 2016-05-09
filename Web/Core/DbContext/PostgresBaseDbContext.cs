using System.Data.Entity;
using PriceAggregator.Core.DbContext.Base;

namespace PriceAggregator.Core.DbContext
{
    public class PostgresBaseDbContext<T> : BaseDbContext where T : class
    {
        public PostgresBaseDbContext()
            : base(nameOrConnectionString: "PriceAggregator")
        {
        }

        public DbSet<T> DbSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
        }
    }
}