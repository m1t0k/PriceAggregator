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
    public class ProductItemRepository : GenericRepository<ProductItem>
    {
        public ProductItemRepository(Lazy<IGenericDbContext> dbContext, Lazy<ICacheRepository> cacheRepository)
            : base(dbContext, cacheRepository)
        {
        }

        public List<ProductItem> GetProductItems()
        {
            return null;//
        }

        public ProductItem GetProductItem(string id)
        {
            return null;//
        }

        public void CreateProductItem(ProductItem item)
        {
            
        }


        public void UpdateProductItem(string id, ProductItem item)
        {
            
        }


        public void DeleteProductItem(string id)
        {
            
        }
    }
}