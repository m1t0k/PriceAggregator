using System;
using System.Collections.Generic;
using PriceAggregator.Core.DataEntity;

namespace PriceAggregator.Core.Interfaces
{
    public interface IReportRepository:IDisposable
    {
        List<RrpSituationReport> GetRrpSituationReport(int categoryId);
        List<RrpViolationReport> GetRrpViolationReport(int categoryId);
    }
}