using System;
using System.Collections.Generic;
using PriceAggregator.Core.Entities;

namespace PriceAggregator.Core.Repository.Interfaces
{
    public interface ICategoryRepository:IDisposable,IDataCache
    {
        List<Category> GetCategories(int id, int? pageIndex, int? pageSize, string sortName);
        List<Category> GetCategories(int id);
        Category GetCategory(int id);
        void CreateCategory(Category category);
        void UpdateCategory(int id, Category category);
        void DeleteCategory(int id);
    }
}