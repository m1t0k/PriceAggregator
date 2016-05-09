using System;
using System.Runtime.Serialization;

namespace PriceAggregator.Core.Entities
{
    [DataContract(Name = "productitem")]
    public class ProductItem : BaseEntity
    {
        [DataMember(Name = "product")]
        public Product Product { get; set; }

        [DataMember(Name = "scanint")]
        public int ScanInterval { get; set; }

        [DataMember(Name = "lastupd")]
        public DateTime? LastUpdated { get; set; }

        [DataMember(Name = "status")]
        public ProductItemStatus Status { get; set; }
    }
}