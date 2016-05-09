using System.Data.Entity;

namespace Web.Core.DbContext.Base
{
    public class PostgresDbContext<T> : BaseDbContext<T> where T : class
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
        }
    }
}