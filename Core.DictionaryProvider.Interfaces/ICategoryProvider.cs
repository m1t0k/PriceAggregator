﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PriceAggregator.Core.DataEntity.Base;

namespace PriceAggregator.Core.DictionaryProvider.Interfaces
{
    public interface IDictionaryProvider<T>:IDisposable where T : BaseEntity
    {
        T GetItem(int id);
        bool CreateItem(T item);
        bool UpdateItem(T item);
        bool DeleteItem(int id);
        IEnumerable<T> GetList(int? pageIndex, int? pageSize, string sortExpression);
        Task<IEnumerable<T>> GetListAsync(int? pageIndex, int? pageSize, string sortExpression);
        Task<int> GetCountAsync();
        int GetCount();
    }
}