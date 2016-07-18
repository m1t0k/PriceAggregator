using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PriceAggregator.Core.DataEntity.Base;

namespace PriceAggregator.Core.DataAccess.Interfaces
{
    public interface IGenericDataAccessProvider<T> : IDisposable where T : BaseEntity
    {
        IEnumerable<T> GetList(int? pageIndex, int? pageSize, string sortName, bool acsending);
        IEnumerable<T> SqlQuery(string sqlQuery, params object[] parameters);
        T GetItem(int id);
        int GetCount();
        void CreateItem(T item);
        void UpdateItem(T item);
        void DeleteItem(int id);

        Task<IEnumerable<T>> GetListAsync(int? pageIndex, int? pageSize, string sortName, bool acsending);
        Task<IEnumerable<T>> SqlQueryAsync(string sqlQuery, params object[] parameters);
        Task<int> GetCountAsync();
        Task<T> GetItemAsync(int id);
        Task<int> CreateItemAsync(T item);
        Task<int> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(int id);
    }
}