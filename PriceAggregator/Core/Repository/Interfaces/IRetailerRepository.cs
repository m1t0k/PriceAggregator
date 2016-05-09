using System;
using System.Collections.Generic;
using PriceAggregator.Core.Entities;

namespace PriceAggregator.Core.Repository.Interfaces
{
    public interface IRetailerRepository : IDisposable, IDataCache
    {
        List<Retailer> GetRetailers(int id, int? pageIndex, int? pageSize, string sortName);
        List<Retailer> GetRetailers();
        Retailer GetRetailer(int id);
        void CreateRetailer(Retailer retailer);
        void UpdateRetailer(int id, Retailer retailer);
        void DeleteRetailer(int id);
    }
}