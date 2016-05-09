using System.ComponentModel.DataAnnotations;
//using Web.App_LocalResources;
using Web.App_LocalResources;

namespace Web.Models
{
    public class RrpViolationReport
    {
        [Key]
        [Display(Name = "Retailer", ResourceType = typeof(RprViolationReport))]
        public string RetailerId { get; set; }

        [Display(Name = "Positions", ResourceType = typeof (RprViolationReport))]
        public int NumberOfPositions { get; set; }

        [Display(Name = "Latest_changes", ResourceType = typeof (RprViolationReport))]
        public int LatestTrends { get; set; }
    }
}