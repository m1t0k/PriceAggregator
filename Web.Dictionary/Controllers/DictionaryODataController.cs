using System;
using System.CodeDom;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using PriceAggregator.Core.DataEntity;
using PriceAggregator.Core.DataEntity.Base;
using Web.Dictionary.Controllers.Base;

namespace Web.Dictionary.Controllers
{
    //[ODataRoutePrefix("DictionaryOData")]
    //[ODataRoute("CategorySet")]
    //[ODataRouteAttribute("")]
    public class DictionaryODataController : ODataController
    {
        private readonly IDynamicExecutorHelper _dynamicExecutor;

        public DictionaryODataController(IDynamicExecutorHelper dynamicExecutor)
        {
            _dynamicExecutor = dynamicExecutor;
        }

        /*
        [ODataRoute("")]
        [EnableQuery(PageSize=10, AllowedQueryOptions = AllowedQueryOptions.Skip |
                               AllowedQueryOptions.Top | AllowedQueryOptions.Count | AllowedQueryOptions.Expand |AllowedQueryOptions.Filter|AllowedQueryOptions.OrderBy|AllowedQueryOptions.Select, AllowedOrderByProperties = "Id,Name", MaxNodeCount = 20)]
        public IQueryable<Category> Get()
        {
            var result =_dynamicExecutor.Execute(Configuration, Request, "Category", "GetAll",
               null);

            return (IQueryable<Category>) result;
        }
        */
        /*
        [ODataRoute("BrandSet")]
        [HttpGet]
        [EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.Skip |
                                                          AllowedQueryOptions.Top | AllowedQueryOptions.Count |
                                                          AllowedQueryOptions.Expand | AllowedQueryOptions.Filter |
                                                          AllowedQueryOptions.OrderBy | AllowedQueryOptions.Select,
            AllowedOrderByProperties = "Id,Name", MaxNodeCount = 20)]
        public IQueryable<Brand> GetBrandSet()
        {
            return (IQueryable<Brand>) _dynamicExecutor.Execute(Configuration, Request, "Brand", "GetAll",
                null);
        }

        [ODataRoute("CategorySet")]
        [EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.Skip |
                                                          AllowedQueryOptions.Top | AllowedQueryOptions.Count |
                                                          AllowedQueryOptions.Expand | AllowedQueryOptions.Filter |
                                                          AllowedQueryOptions.OrderBy | AllowedQueryOptions.Select,
            AllowedOrderByProperties = "Id,Name", MaxNodeCount = 20)]
        public IQueryable<Category> GetCategorySet()
        {
          // throw  new Exception("test");
            return (IQueryable<Category>) _dynamicExecutor.Execute(Configuration, Request, "Category", "GetAll",
                null);
        }
        */
        [ODataRoute("EntitySet")]
        [EnableQuery(PageSize = 10,  AllowedQueryOptions = AllowedQueryOptions.All,
            AllowedOrderByProperties = "Id,Name", MaxNodeCount = 20)]
        public IQueryable<BaseEntity> Get()
        {
            return (IQueryable<BaseEntity>) _dynamicExecutor.Execute(Configuration, Request, "Category", "GetAll",
                null);
        }

        [ODataRoute("GetEntitySet(typename={typeName)")]
        [EnableQuery(PageSize = 10,  AllowedQueryOptions = AllowedQueryOptions.All,
            AllowedOrderByProperties = "Id,Name", MaxNodeCount = 20)]
        public IQueryable<BaseEntity> GetEntitySet([FromODataUri]string typeName)
        {
            return (IQueryable<BaseEntity>) _dynamicExecutor.Execute(Configuration, Request, typeName, "GetAll",
                null);
        }


        /*
        [ODataRoute("/id")]
        [EnableQuery()]
        public SingleResult<Category> GetCategory([FromODataUri] int id)
        {
            return SingleResult.Create(_list.Where(item => item.Id == id).AsQueryable());
        }
        */
    }
}