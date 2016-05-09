using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Npgsql;

namespace PriceAggregator.Core
{
    public class NpgsqlConnection : DbConfiguration
    {
        public NpgsqlConnection()
        {
            SetExecutionStrategy("Npgsql", () => new DefaultExecutionStrategy());

            SetDefaultConnectionFactory(new NpgsqlConnectionFactory());
        }
    }
}