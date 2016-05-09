using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RrpSituationReport
    {
        [Key]
        public int NumberOfPositionsBellowRRp { get; set; }
        public int LatestBellowRrpTrends { get; set; }
        public int NumberOfPositionsAboveRRp { get; set; }
        public int LatestAboveRrpTrends { get; set; }
        public int NumberOfNaPositions { get; set; }
        public int LatestNaTrends { get; set; }
    }
}