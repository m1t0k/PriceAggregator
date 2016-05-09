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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(Lazy<IGenericDbContext> dbContext, Lazy<ICacheRepository> cacheRepository)
            : base(dbContext,cacheRepository)
        {
        }

        public List<Category> GetCategories(int id, int? pageIndex, int? pageSize, string sortName)
        {
            return GetList(id, pageIndex, pageSize, sortName);
        }

        public List<Category> GetCategories(int id)
        {
            return GetCategories(id, null, null, null);
        }

        public Category GetCategory(int id)
        {
            return GetItem(id);
        }

        public void CreateCategory(Category category)
        {
            CreateItem(category.Id, category);
        }

        public void UpdateCategory(int id, Category category)
        {
            UpdateItem(id, category);
        }

        public void DeleteCategory(int id)
        {
            DeleteItem(id);
        }
    }
}