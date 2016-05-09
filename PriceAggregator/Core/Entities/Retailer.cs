using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriceAggregator.Core.Entities
{
    [Table("retailer", Schema = "dbo")]
    public class Retailer : BaseEntity
    {
        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("url")]
        public string Url { get; set; }


        [Column("description")]
        public string Description { get; set; }
    }
}