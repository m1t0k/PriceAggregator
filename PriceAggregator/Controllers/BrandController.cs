using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using PriceAggregator.Controllers.Base;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Filters;
using PriceAggregator.Core.Repository.Interfaces;

namespace PriceAggregator.Controllers
{
    [System.Web.Http.RoutePrefix("api/brand")]
    public class BrandController : BaseController
    {
        private readonly IBrandRepository _repository;

        public BrandController(IBrandRepository repository)
        {
            _repository = repository;
        }
        
        [OutputCache]
        [System.Web.Http.Route("list")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetBrandList()
        {
            return GetBrandList(null, null, null);
        }

        [System.Web.Http.Route("list/{pageIndex:int}")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetBrandList(int pageIndex)
        {
            return GetBrandList(pageIndex, null, null);
        }
        
        [System.Web.Http.Route("list/{pageIndex:int}/{  pageSize:int}")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetBrandList(int pageIndex, int pageSize)
        {
            return GetBrandList(pageIndex, pageSize, null);
        }
        

        [System.Web.Http.Route("list/{pageIndex:int}/{pageSize:int}/{sortName}")]
        [System.Web.Http.HttpGet]
        public /*async Task<IHttpActionResult>*/ IHttpActionResult GetBrandList(int? pageIndex, int? pageSize, string sortName = null)
        {
            List<Brand> brands = null;
                using (var dbProvider = _repository)
                {
                  brands= dbProvider.GetBrands(0, pageIndex, pageSize, sortName);
                }
            

            if (brands.Count == 0)
            {
                return NotFound();
            }

            return Ok(brands);
        }

        [System.Web.Http.Route("{id}")]
        [System.Web.Http.HttpGet]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult GetBrand(int id)
        {
            using (IBrandRepository dbProvider = _repository)
            {
                Brand brand = dbProvider.GetBrand(id);
                return Ok(brand);
            }
        }


        [System.Web.Http.HttpPost]
        [ObjectAlreadyExistsFilter]
        public IHttpActionResult Post([FromBody] Brand brand)
        {
            using (IBrandRepository dbProvider = _repository)
            {
                dbProvider.CreateBrand(brand);
                return Ok();
            }
        }

        [System.Web.Http.Route("{id:int}")]
        [System.Web.Http.HttpPut]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Put(int id, [FromBody] Brand brand)
        {
            using (IBrandRepository dbProvider = _repository)
            {
                dbProvider.UpdateBrand(id, brand);
                return Ok();
            }
        }

        [System.Web.Http.Route("{id}")]
        [System.Web.Http.HttpDelete]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Delete(int id)
        {
            using (IBrandRepository dbProvider = _repository)
            {
                dbProvider.DeleteBrand(id);
                return Ok();
            }
        }
    }
}