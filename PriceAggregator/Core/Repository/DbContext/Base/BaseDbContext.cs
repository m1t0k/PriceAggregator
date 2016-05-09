using System;
using System.Collections.Generic;
using System.Linq;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Libraries.Logging;

namespace PriceAggregator.Core.Repository.DbContext.Base
{
    public abstract class BaseDbContext : System.Data.Entity.DbContext, IBaseDbContext
    {
        protected ILoggingService Logger = null;

        protected BaseDbContext(ILoggingService logger)
            : base("PriceAggregator")
        {
            //Configuration.AutoDetectChangesEnabled = false;
            
            Logger = logger;
            
            if(Logger.IsTraceEnabled)
                this.Database.Log= s => Logger.Trace(s);
        }

        public List<T> SqlQuery<T>(string sqlQuery, params object[] parameters) where T : BaseEntity
        {
            return Database.SqlQuery<T>(sqlQuery, parameters).AsQueryable().ToList();
        }

        public new void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}