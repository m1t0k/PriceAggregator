using System.Data.Entity;
using PriceAggregator.Models;

namespace PriceAggregator.Core.DbContext
{
    public class ProductItemDbContext : PostgresBaseDbContext<ProductItem>
    {
    }
}