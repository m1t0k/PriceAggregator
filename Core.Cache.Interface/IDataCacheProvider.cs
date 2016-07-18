using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PriceAggregator.Core.DataEntity.Base;

namespace PriceAggregator.Core.Cache.Interface
{
    public interface IDataCacheProvider<T>:IDisposable where T : BaseEntity
    {
        string GenerateKey(T item);
        string GenerateKey(int itemId);

        T GetItem(int itemId);
        IEnumerable<T> GetItems(IEnumerable<int> itemIds);

        bool AddItem(T item);
        bool AddItems(IEnumerable<T> items);

        bool ReplaceItem(T item);
        List<Tuple<int, bool>> ReplaceItems(IEnumerable<T> items);

        bool RemoveItem(int itemId);
        void RemoveItems(IEnumerable<int> itemIds);


        Task<T> GetItemAsync(int itemId);
        Task<IEnumerable<T>> GetItemsAsync(IEnumerable<int> itemIds);

        Task<bool> AddItemAsync(T item);
        Task<bool> AddItemsAsync(IEnumerable<T> items);

        Task<bool> ReplaceItemAsync(T item);
        List<Tuple<int, Task<bool>>> ReplaceItemsAsync(IEnumerable<T> items);

        Task<bool> RemoveItemAsync(int itemId);
        Task RemoveItemsAsync(IEnumerable<int> itemIds);
       
    }
}