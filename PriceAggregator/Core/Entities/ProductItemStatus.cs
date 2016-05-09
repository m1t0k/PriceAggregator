using System.Runtime.Serialization;

namespace PriceAggregator.Core.Entities
{
    [DataContract(Name = "ProductItemStatus")]
    public enum ProductItemStatus
    {
        [EnumMember] Disabled = 0,
        [EnumMember] Enabled = 1
    }
}