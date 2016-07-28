using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PriceAggregator.Core.DataEntity.Base;

namespace PriceAggregator.Core.DataEntity
{
    [Table("brand", Schema = "dbo")]
    public class Brand : BaseEntity
    {
        [Required]
        [Column("name")]
        [Display(Name = "Name" /*, ResourceType = typeof(Resources.Resources)*/)]
        public string Name { get; set; }

        [Required]
        [Column("description")]
        [Display(Name = "Description" /*,  ResourceType = typeof(Resources.Resources)*/)]
        public string Description { get; set; }


        public override string ToString()
        {
            return $"[Id={Id},Name={Name},Description={Description}]";
        }
    }
}