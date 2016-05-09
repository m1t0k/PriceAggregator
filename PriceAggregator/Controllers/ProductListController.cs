using System.Collections.Generic;
using System.Web.Http;
using PriceAggregator.Controllers.Base;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Filters;
using PriceAggregator.Core.Repository;

namespace PriceAggregator.Controllers
{
    [RoutePrefix("api/productlist")]
    public class ProductListController : BaseController
    {
        private readonly ProductRepository _repository;
        
        public ProductListController(ProductRepository repository)
        {
            _repository = repository;
        }

        [Route("list")]
        [HttpGet]
        public IHttpActionResult GetProductLists()
        {
            using (var dbProvider = _repository)
            {
                List<Product> products = dbProvider.GetProducts();
                if (products.Count == 0)
                {
                    return NotFound();
                }
                return Ok(products);
            }
        }

        [Route("{id}")]
        [HttpGet]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult GetProductList(string id)
        {
            var dbProvider = _repository;
            Product product = dbProvider.GetProduct(id);
            return Ok(product);
        }


        [HttpPost]
        [ObjectAlreadyExistsFilter]
        public IHttpActionResult Post([FromBody] Product product)
        {
            var dbProvider = _repository;
            dbProvider.CreateProduct(product);
            return Ok();
        }

        [Route("{id}")]
        [HttpPut]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Put(string id, [FromBody] Product product)
        {
            var dbProvider = _repository;
            dbProvider.UpdateProduct(id, product);
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Delete(string id)
        {
            var dbProvider = _repository;
            dbProvider.DeleteProduct(id);
            return Ok();
        }
    }
}