using System;
using System.Data.Entity.Infrastructure;

namespace PriceAggregator.Core.DataProvider
{
    public class BaseDataProvider
    {
        protected IObjectContextAdapter _dbContext = null;

        public BaseDataProvider()
        {
        }
        
        public BaseDataProvider(IObjectContextAdapter dbContext)
        {
            _dbContext = dbContext;
        }


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.ObjectContext.Dispose();
                    _dbContext = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}