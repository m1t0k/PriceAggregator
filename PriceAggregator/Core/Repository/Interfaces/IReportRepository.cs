using System;
using System.Collections.Generic;
using PriceAggregator.Core.Entities;

namespace PriceAggregator.Core.Repository.Interfaces
{
    public interface IReportRepository:IDisposable
    {
        List<RrpSituationReport> GetRrpSituationReport(int categoryId);
        List<RrpViolationReport> GetRrpViolationReport(int categoryId);
    }
}