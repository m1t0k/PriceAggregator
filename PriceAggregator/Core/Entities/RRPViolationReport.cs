//using Web.App_LocalResources;

namespace PriceAggregator.Core.Entities
{
    public class RrpViolationReport : BaseEntity
    {
        public string RetailerId { get; set; }

        public int NumberOfPositions { get; set; }

        public int LatestTrends { get; set; }
    }
}