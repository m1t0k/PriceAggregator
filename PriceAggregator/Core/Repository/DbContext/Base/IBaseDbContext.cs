using System;
using System.Collections.Generic;
using PriceAggregator.Core.Entities;

namespace PriceAggregator.Core.Repository.DbContext.Base
{
    public interface IBaseDbContext : IDisposable
    {
        List<T> SqlQuery<T>(string sqlQuery, params object[] parameters) where T : BaseEntity;
    }
}