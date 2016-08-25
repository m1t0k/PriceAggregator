using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using PriceAggregator.Core.DataEntity.Base;
using Web.Dictionary.Controllers.Base;
using Web.Dictionary.Models;

namespace Web.Dictionary.Controllers
{
    public class DictionaryODataController : ODataController
    {
        private readonly IDynamicExecutorHelper _dynamicExecutor;

        public DictionaryODataController(IDynamicExecutorHelper dynamicExecutor)
        {
            _dynamicExecutor = dynamicExecutor;
        }


        [ODataRoute("Types")]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Select)]
        public IQueryable<DictionaryType> Get()
        {
            return
                _dynamicExecutor.GetSupportedTypes(Configuration)
                    .Select(item => new DictionaryType {Name = item.Name})
                    .AsQueryable();
        }

        [ODataRoute("GetEntitySet(typeName={typeName})")]
        [EnableQuery( AllowedQueryOptions = AllowedQueryOptions.All,
            AllowedOrderByProperties = "Id,Name", MaxNodeCount = 20)]
        public IQueryable<BaseEntity> GetEntitySet([FromODataUri] string typeName)
        {
            return (IQueryable<BaseEntity>) _dynamicExecutor.Execute(Configuration, Request, typeName, "GetAll",
                null);
        }
    }
}