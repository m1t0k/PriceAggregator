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
        int CreateItem(T item);
        int UpdateItem(T item);
        int DeleteItem(int id);
        int CreateItems(IEnumerable<T> items);

        Task<IEnumerable<T>> GetListAsync(int? pageIndex, int? pageSize, string sortName, bool acsending);
        Task<IEnumerable<T>> SqlQueryAsync(string sqlQuery, params object[] parameters);
        Task<int> GetCountAsync();
        Task<T> GetItemAsync(int id);
        Task<int> CreateItemAsync(T item);
        Task<int> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(int id);
    }
}