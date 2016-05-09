using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.ModelBinding;

namespace Web.Models
{
    [Table("brand", Schema = "dbo")]
    public class Brand
    {
        [Column ("id")]
   
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [Display(Name = "Name"/*, ResourceType = typeof(Resources.Resources)*/)]
        public string Name { get; set; }

        [Required]
        [Column("description")]
        [Display(Name = "Description"/*,  ResourceType = typeof(Resources.Resources)*/)]
        public string Description { get; set; }


        public override string ToString()
        {
            return String.Format("[Id={0},Name={1},Description={2}]", Id, Name, Description);
        }   
    }
}