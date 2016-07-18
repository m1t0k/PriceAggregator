using System;
using System.Collections.Generic;
using PriceAggregator.Core.DataEntity;

namespace PriceAggregator.Core.Interfaces
{
    public interface IRetailerRepository : IDataCache
    {
        List<Retailer> GetRetailers(int id, int? pageIndex, int? pageSize, string sortName);
        List<Retailer> GetRetailers();
        Retailer GetRetailer(int id);
        void CreateRetailer(Retailer retailer);
        void UpdateRetailer(int id, Retailer retailer);
        void DeleteRetailer(int id);
    }
}