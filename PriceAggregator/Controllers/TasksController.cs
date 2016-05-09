using System.Web.Http;
using PriceAggregator.Controllers.Base;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Filters;
using PriceAggregator.Core.Repository;

namespace PriceAggregator.Controllers
{
    [RoutePrefix("api/task")]
    public class TasksController : BaseController
    {
        private readonly PriceTaskRepository _repository;
        
        public TasksController(PriceTaskRepository repository)
        {
            _repository = repository;
        }
        
        [Route("list/{status:pricetaskstatus}/{limit:int}")]
        [HttpGet]
        public IHttpActionResult GetTasks(PriceTaskStatus status, int limit)
        {
            var dbProvider = _repository;
            PriceTaskCollection tasks = dbProvider.GetTasks(status, limit);
            if (tasks.Items.Count == 0)
            {
                return NotFound();
            }
            return Ok(tasks);
        }


        [Route("{id}")]
        [HttpGet]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult GetTask(string id)
        {
            var dbProvider = _repository;
            PriceTask task = dbProvider.GetTask(id);
            return Ok(task);
        }

        [HttpPost]
        [ObjectAlreadyExistsFilter]
        public IHttpActionResult Post([FromBody] PriceTask task)
        {
            //if (brand == null) throw new ArgumentNullException("brand");
            var dbProvider = _repository;
            dbProvider.CreateTask(task);
            return Ok();
        }

        [Route("{id}")]
        [HttpPut]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Put(string id, [FromBody] PriceTask task)
        {
            var dbProvider = _repository;
            dbProvider.UpdateTask(id, task);
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        [ObjectDoesNotExistFilter]
        public IHttpActionResult Delete(string id)
        {
            var dbProvider = _repository;
            dbProvider.DeleteTask(id);
            return Ok();
        }
    }
}