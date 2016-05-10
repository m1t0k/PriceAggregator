using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace PriceAggregator.Tests.Controllers
{
    [TestClass]
    public class CategoryControllerTest
    {

        /*  [Route("list")]
          [HttpGet]
          public IHttpActionResult GetCategoryList_ShouldReturnList()
          {
              using (CategoryRepository dbProvider = new CategoryRepository())
              {
                  List<Category> categories = dbProvider.GetCategories();
                  if (categories.Count == 0)
                  {
                      return NotFound();
                  }
                  return Ok(categories);
              }
          }


          [Route("{id}")]
          [HttpGet]
          [ObjectDoesNotExistFilter]
          public IHttpActionResult GetCategory_ShouldReturnCorrectCategory(int id)
          {
              using (CategoryRepository dbProvider = _dataProvider)
              {
                  Category category = dbProvider.GetCategory(id);
                  return Ok(category);
              }
          }


          [HttpPost]
          [ObjectAlreadyExistsFilter]
          public IHttpActionResult Post_ShouldCreateCorrectCategory([FromBody] Category category)
          {
              using (CategoryRepository dbProvider = _dataProvider)
              {
                  dbProvider.CreateCategory(category);
                  return Ok();
              }
          }

          [Route("{id}")]
          [HttpPut]
          [ObjectDoesNotExistFilter]
          public IHttpActionResult Put_ShouldUpdateCategory(string id, [FromBody] Category category)
          {
              using (CategoryRepository dbProvider = _dataProvider)
              {
                  dbProvider.UpdateCategory(id, category);
                  return Ok();
              }
          }

          [Route("{id}")]
          [HttpDelete]
          [ObjectDoesNotExistFilter]
          public IHttpActionResult Delete_ShouldDeleteCategory(string id)
          {
              using (CategoryRepository dbProvider = _dataProvider)
              {
                  dbProvider.DeleteCategory(id);
                  return Ok();
              }
          }*/
    }
}
