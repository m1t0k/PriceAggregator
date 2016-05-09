using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.App_LocalResources;

namespace Web.Models
{
    [Table("category", Schema = "dbo")]
    public class Category
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("name")]
        [Display(Name = "Name", ResourceType = typeof(Web.App_LocalResources.Category))]
        public string Name { get; set; }

        [Column("description")]
        [Display(Name = "Description", ResourceType = typeof(Web.App_LocalResources.Category))]
        public string Description { get; set; }
    }
}