using System;
using System.Collections.Generic;
using PriceAggregator.Core.Caching;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Repository.Base;
using PriceAggregator.Core.Repository.DbContext.Base;
using PriceAggregator.Core.Repository.Interfaces;

namespace PriceAggregator.Core.Repository.MsSql
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(Lazy<IGenericDbContext> dbContext, Lazy<ICacheRepository> cacheRepository)
            : base(dbContext, cacheRepository)
        {
        }

        public List<Brand> GetBrands(int id, int? pageIndex, int? pageSize, string sortName)
        {
            return GetList(id, pageIndex, pageSize, sortName);
        }

        public List<Brand> GetBrands()
        {
            return GetList(0, null, null, null);
        }

        public Brand GetBrand(int id)
        {
            return GetItem(id);
        }

        public void CreateBrand(Brand brand)
        {
            CreateItem(brand.Id, brand);
        }


        public void UpdateBrand(int id, Brand brand)
        {
            UpdateItem(brand.Id, brand);
        }


        public void DeleteBrand(int id)
        {
            DeleteItem(id);
        }
    }
}