using System;
using System.Collections.Generic;
using PriceAggregator.Core.Entities;

namespace PriceAggregator.Core.Repository.Interfaces
{
    public interface IBrandRepository : IDisposable, IDataCache
    {
        List<Brand> GetBrands(int id, int? pageIndex, int? pageSize, string sortName);
        List<Brand> GetBrands();
        Brand GetBrand(int id);
        void CreateBrand(Brand brand);
        void UpdateBrand(int id, Brand brand);
        void DeleteBrand(int id);
    }
}