using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using PriceAggregator.Core.Caching;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.ExceptionHandling;
using PriceAggregator.Core.Repository.Base;
using PriceAggregator.Core.Repository.DbContext.Base;

namespace PriceAggregator.Core.Repository
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(Lazy<IGenericDbContext> dbContext, Lazy<ICacheRepository> cacheRepository)
            : base(dbContext, cacheRepository)
        {
        }

        public List<Product> GetProducts()
        {
            return null;// return DbContext.DbSet.AsQueryable().OrderBy(item => item.Name).ToList();
        }

        public Product GetProduct(string id)
        {
           /* Product product = DbContext.DbSet.Find(id);

            if (product == null)
            {
                throw new ObjectDoesNotExistException();
            }

            return product;
            * */
            return null;//
        }

        public void CreateProduct(Product product)
        {
           
        }


        public void UpdateProduct(string id, Product product)
        {
                   }


        public void DeleteProduct(string id)
        {
                   }
    }
}