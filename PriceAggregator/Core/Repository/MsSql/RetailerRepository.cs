using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using PriceAggregator.Core.Caching;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Repository.Base;
using PriceAggregator.Core.Repository.DbContext.Base;
using PriceAggregator.Core.Repository.Interfaces;

namespace PriceAggregator.Core.Repository.MsSql
{
    public class RetailerRepository : GenericRepository<Retailer>, IRetailerRepository
    {
        public RetailerRepository(Lazy<IGenericDbContext> dbContext, Lazy<ICacheRepository> cacheRepository)
            : base(dbContext,cacheRepository)
        {
        }

        public List<Retailer> GetRetailers(int id, int? pageIndex, int? pageSize, string sortName)
        {
            return GetList(id, pageIndex, pageSize, sortName);
        }

        public List<Retailer> GetRetailers()
        {
            return GetList(0, null, null, null);
        }

        public Retailer GetRetailer(int id)
        {
            return GetItem(id);
        }

        public void CreateRetailer(Retailer retailer)
        {
            CreateItem(retailer.Id, retailer);
        }


        public void UpdateRetailer(int id, Retailer retailer)
        {
            UpdateItem(retailer.Id, retailer);
        }


        public void DeleteRetailer(int id)
        {
            DeleteItem(id);
        }
    }
}