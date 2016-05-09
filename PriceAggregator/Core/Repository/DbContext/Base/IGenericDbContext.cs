using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PriceAggregator.Core.Entities;

namespace PriceAggregator.Core.Repository.DbContext.Base
{
    public interface IGenericDbContext : IBaseDbContext
    {
        List<T> GetList<T>(int id, int? pageIndex, int? pageSize, string sortName) where T : BaseEntity;

        List<T> GetList<T>(IQueryable<T> items, int id, int? pageIndex, int? pageSize, string sortName)
            where T : BaseEntity;

        T GetItem<T>(int id) where T : BaseEntity;
        void CreateItem<T>(int id, T item) where T : BaseEntity;
        void UpdateItem<T>(int id, T item) where T : BaseEntity;
        void DeleteItem<T>(int id) where T : BaseEntity;
        DbSet GetDbSet<T>() where T : BaseEntity;
    }
}