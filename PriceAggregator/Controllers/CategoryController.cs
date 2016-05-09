using System.Collections.Generic;
using System.Web.Http;
using PriceAggregator.Controllers.Base;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Filters;
using PriceAggregator.Core.Repository.Interfaces;

namespace PriceAggregator.Controllers
{
    [RoutePrefix("api/category")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }


        [Route("list")]
        [HttpGet]
        public  IHttpActionResult GetCategoryList()
        {
            return  GetCategoryList(null, null, null);
        }

        [Route("list/{pageIndex:int}")]
        [HttpGet]
        public  IHttpActionResult GetCategoryList(int pageIndex)
        {
            return  GetCategoryList(pageIndex, null, null);
        }

        [Route("list/{pageIndex:int}/{pageSize:int}")]
        [HttpGet]
        public  IHttpActionResult GetCategoryList(int pageIndex, int pageSize)
        {
            return  GetCategoryList(pageIndex, pageSize, null);
        }

        [Route("list/{pageIndex:int}/{pageSize:int}/{sortName}")]
        [HttpGet]
        public IHttpActionResult GetCategoryList(int? pageIndex=null,int? pageSize=null,string sortName=null)
        {
            using (var dbProvider = _repository)
            {
                List<Category> categories = dbProvider.GetCategories(0,pageIndex,pageSize,sortName);
                if (categories.Count == 0)
                {
                    return NotFound();
                }
                return Ok(categories);
            }
        }


        [Route("{id:int}")]
        [HttpGet]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult GetCategory(int id)
        {
            using (var repository = _repository)
            {
                Category category = repository.GetCategory(id);
                return Ok(category);
            }
        }

        [Route("")]
        [HttpPost]
        [ObjectAlreadyExistsFilter]
        public IHttpActionResult Post([FromBody] Category category)
        {
            using (var dbProvider = _repository)
            {
                dbProvider.CreateCategory(category);
                return Ok();
            }
        }

        [Route("{id:int}")]
        [HttpPut]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Put(int id, [FromBody] Category category)
        {
            using (var dbProvider = _repository)
            {
                dbProvider.UpdateCategory(id, category);
                return Ok();
            }
        }

        [Route("{id:int}")]
        [HttpDelete]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Delete(int id)
        {
            using (var dbProvider = _repository)
            {
                dbProvider.DeleteCategory(id);
                return Ok();
            }
        }
    }
}