using System.Threading.Tasks;
using PriceAggregator.Core.DataEntity.Base;
using RestSharp;

namespace PriceAggregator.Web.BusinessLogic.Helpers
{
    public interface IDictionaryRestClient
    {
        Task<IRestResponse> GetListAsync(string typeName, int? pageIndex, int? pageSize, string sortExpression);
        Task<IRestResponse> GetCountAsync(string typeName);
        Task<IRestResponse> GetTypesAsync();
        Task<IRestResponse> CreateItemAsync(string typeName, dynamic item);
        Task<IRestResponse> UpdateItemAsync(string typeName, int id, dynamic item);
        Task<IRestResponse> DeleteItemAsync(string typeName, int id);
        Task<IRestResponse> GetItemAsync(string typeName, int id);
    }
}