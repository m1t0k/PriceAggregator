using Web.Core.DbContext.Base;
using Web.Models;


namespace Web.Core.DbContext
{
    public class ProductDbContext : BaseDbContext<Product>
    {

        public System.Data.Entity.DbSet<Web.Models.Brand> Brands { get; set; }

        public System.Data.Entity.DbSet<Web.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Web.Models.ProductList> ProductLists { get; set; }
    }
}