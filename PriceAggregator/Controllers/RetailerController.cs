using System.Collections.Generic;
using System.Web.Http;
using PriceAggregator.Controllers.Base;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Filters;
using PriceAggregator.Core.Repository.Interfaces;

namespace PriceAggregator.Controllers
{
    [RoutePrefix("api/retailer")]
    public class RetailerController : BaseController
    {
        private readonly IRetailerRepository _repository;

        public RetailerController(IRetailerRepository repository)
        {
            _repository = repository;
        }


        [Route("list")]
        [HttpGet]
        public  IHttpActionResult GetRetailerList()
        {
            return  GetRetailerList(null, null, null);
        }

        [Route("list/{pageIndex:int}")]
        [HttpGet]
        public  IHttpActionResult GetRetailerList(int pageIndex)
        {
            return  GetRetailerList(pageIndex, null, null);
        }

        [Route("list/{pageIndex:int}/{pageSize:int}")]
        [HttpGet]
        public  IHttpActionResult GetRetailerList(int pageIndex, int pageSize)
        {
            return GetRetailerList(pageIndex, pageSize, null);
        }

        [Route("list/{pageIndex:int}/{pageSize:int}/{sortName}")]
        [HttpGet]
        public IHttpActionResult GetRetailerList(int? pageIndex, int? pageSize, string sortName = null)
        {
            using (var dbProvider = _repository)
            {
                List<Retailer> retailers = dbProvider.GetRetailers(0, pageIndex, pageSize, sortName);
                if (retailers.Count == 0)
                {
                    return NotFound();
                }
                return Ok(retailers);
            }
        }

        [Route("{id}")]
        [HttpGet]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult GetRetailer(int id)
        {
            using (var dbProvider = _repository)
            {
                Retailer retailer = dbProvider.GetRetailer(id);
                return Ok(retailer);
            }
        }


        [HttpPost]
        [ObjectAlreadyExistsFilter]
        public IHttpActionResult Post([FromBody] Retailer retailer)
        {
            using (var dbProvider = _repository)
            {
                dbProvider.CreateRetailer(retailer);
                return Ok();
            }
        }

        [Route("{id:int}")]
        [HttpPut]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Put(int id, [FromBody] Retailer retailer)
        {
            using (var dbProvider = _repository)
            {
                dbProvider.UpdateRetailer(id, retailer);
                return Ok();
            }
        }

        [Route("{id}")]
        [HttpDelete]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Delete(int id)
        {
            using (var dbProvider = _repository)
            {
                dbProvider.DeleteRetailer(id);
                return Ok();
            }
        }
    }
}