using System.Collections.Generic;
using System.Threading.Tasks;
using PriceAggregator.Core.DataEntity.Base;

namespace PriceAggregator.Web.BusinessLogic.Helpers
{
    public interface IDictionaryRestClient
    {
        T GetItem<T>(int id) where T : BaseEntity;
        void CreateItem<T>(T item) where T : BaseEntity;
        void UpdateItem<T>(T item) where T : BaseEntity;
        void DeleteItem<T>(int id) where T : BaseEntity;
        IEnumerable<T> GetList<T>(int? pageIndex, int? pageSize, string sortExpression) where T : BaseEntity;

        Task<IEnumerable<T>> GetListAsync<T>(int? pageIndex, int? pageSize, string sortExpression)
            where T : BaseEntity;

        Task<int> GetCountAsync<T>() where T : BaseEntity;
        int GetCount<T>() where T : BaseEntity;
    }
}