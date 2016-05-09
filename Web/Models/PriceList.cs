using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models
{
    [Table("pricelist", Schema = "dbo")]
    public class PriceList
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("retailerid")]
        public int RetailerId { get; set; }

        [Column("productid")]
        public int ProductId { get; set; }

        [Column("price")]
        public double Price { get; set; }

        [Column("lastupdated")]
        public DateTime LastUpdated { get; set; }

        [Column("status")]
        public short Status { get; set; }
 
    }
}