//using Web.App_LocalResources;

using PriceAggregator.Core.DataEntity.Base;

namespace PriceAggregator.Core.DataEntity
{
    public class RrpViolationReport : BaseEntity
    {
        public string RetailerId { get; set; }

        public int NumberOfPositions { get; set; }

        public int LatestTrends { get; set; }
    }
}