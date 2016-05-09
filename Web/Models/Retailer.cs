using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models
{
    
    [Table("retailer", Schema = "dbo")]
    public class Retailer
    {
        [Key]
        [Column ("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [Display(Name = "Name"/*, ResourceType = typeof(Resources.Resources)*/)]
        public string Name { get; set; }


        [Required]
        [Column("url")]
        [Display(Name = "Url"/*, ResourceType = typeof(Resources.Resources)*/)]
        public string Url { get; set; }

        
        [Column("description")]
        [Display(Name = "Description"/*,  ResourceType = typeof(Resources.Resources)*/)]
        public string Description { get; set; }
   
    }
}