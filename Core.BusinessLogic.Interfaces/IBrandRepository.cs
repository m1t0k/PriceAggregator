using System;
using System.Collections.Generic;
using PriceAggregator.Core.DataEntity;

namespace PriceAggregator.Core.Interfaces
{
    public interface IBrandRepository : IDataCache
    {
        List<Brand> GetBrands(int id, int? pageIndex, int? pageSize, string sortName);
        List<Brand> GetBrands();
        Brand GetBrand(int id);
        void CreateBrand(Brand brand);
        void UpdateBrand(int id, Brand brand);
        void DeleteBrand(int id);
    }
}