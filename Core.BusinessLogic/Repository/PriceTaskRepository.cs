using System;
using System.Data.Entity.Migrations;
using System.Linq;
using PriceAggregator.Core.Caching;
using PriceAggregator.Core.DataAccess.DbContext.Base;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.ExceptionHandling;
using PriceAggregator.Core.Repository.Base;

namespace PriceAggregator.Core.Repository
{
    public class PriceTaskRepository : GenericRepository<PriceTask>
    {
        public PriceTaskRepository(Lazy<IGenericDbContext> dbContext, Lazy<ICacheRepository> cacheRepository)
            : base(dbContext, cacheRepository)

        {
        }

        public PriceTaskCollection GetTasks(PriceTaskStatus status, int limit)
        {
            return null;//
        }


        public PriceTask GetTask(string id)
        {
            return null;//
        }

        public void CreateTask(PriceTask task)
        {
            
        }

        public void UpdateTask(string id, PriceTask task)
        {
            
        }

        public void DeleteTask(string id)
        {
            
        }
    }
}