using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriceAggregator.Core.DataEntity
{
    [Table("productlist", Schema = "dbo")]
    public class ProductList
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("ownerid")]
        public int OwnerId { get; set; }


        [Key]
        [ForeignKey("Brand")]
        [Column("brandid")]
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("lastupdated")]
        public DateTime LastUpdated { get; set; }
    }
}