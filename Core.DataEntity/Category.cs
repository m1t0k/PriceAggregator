using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PriceAggregator.Core.DataEntity.Base;

namespace PriceAggregator.Core.DataEntity
{
    [Table("category", Schema = "dbo")]
    public class Category : BaseEntity, IEquatable<Category>
    {
        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        public bool Equals(Category category)
        {
            return category != null && category.Id == Id && category.Name.Equals(Name) &&
                   category.Description.Equals(Description);
        }
    }
}