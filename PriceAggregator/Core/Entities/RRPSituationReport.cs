namespace PriceAggregator.Core.Entities
{
    public class RrpSituationReport : BaseEntity
    {
        public int NumberOfPositionsBellowRRp { get; set; }
        public int LatestBellowRrpTrends { get; set; }
        public int NumberOfPositionsAboveRRp { get; set; }
        public int LatestAboveRrpTrends { get; set; }
        public int NumberOfNaPositions { get; set; }
        public int LatestNaTrends { get; set; }
    }
}