using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PriceAggregator.Core.DataEntity.Base;

namespace PriceAggregator.Core.DataEntity
{
    /*
     create table public.product
(
  id serial primary key,
  sku varchar(512) not null,
  name varchar(512) not null, 
  rrp money,
  brandId integer not null,
  categoryId integer not null,
  productListId integer not null,
  createdAt timestamp not  null,
  lastUpdated timestamp 
)
     */

    [Table("product", Schema = "dbo")]
    public class Product : BaseEntity
    {
        [Column("sku")]
        [Required]
        public string Sku { get; set; }

        [Column("rrp")]
        public decimal? Rrp { get; set; }


        [ForeignKey("Brand")]
        [Column("brandid")]
        [Required]
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }


        [ForeignKey("Category")]
        [Column("categoryid")]
        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }


        [ForeignKey("ProductList")]
        [Column("productlistid")]
        public int ProductListId { get; set; }

        public virtual ProductList ProductList { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("lastupdated")]
        public DateTime? LastUpdated { get; set; }
    }
}