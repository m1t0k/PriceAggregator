using System.Collections.Generic;
using System.Web.Http;
using PriceAggregator.Controllers.Base;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Filters;
using PriceAggregator.Core.Repository;

namespace PriceAggregator.Controllers
{
    [RoutePrefix("api/item")]
    public class ProductItemController : BaseController
    {
        private readonly ProductItemRepository _repository;
        
        public ProductItemController(ProductItemRepository repository)
        {
            _repository = repository;
        }

        [Route("list")]
        [HttpGet]
        public IHttpActionResult GetProductItems()
        {
            var dbProvider = _repository;
            List<ProductItem> items = dbProvider.GetProductItems();
            if (items.Count == 0)
            {
                return NotFound();
            }

            return Ok(items);
        }

        [Route("{id}")]
        [HttpGet]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult GetProductItem(string id)
        {
            var dbProvider = _repository;
            ProductItem productItem = dbProvider.GetProductItem(id);
            return Ok(productItem);
        }


        [HttpPost]
        [ObjectAlreadyExistsFilter]
        public IHttpActionResult Post([FromBody] ProductItem productItem)
        {
            var dbProvider = _repository;
            dbProvider.CreateProductItem(productItem);
            return Ok();
        }

        [Route("{id}")]
        [HttpPut]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Put(string id, [FromBody] ProductItem productItem)
        {
            var dbProvider = _repository;
            dbProvider.UpdateProductItem(id, productItem);
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Delete(string id)
        {
            var dbProvider = _repository;
            dbProvider.DeleteProductItem(id);
            return Ok();
        }
    }
}