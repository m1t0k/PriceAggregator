using System.Runtime.Serialization;

namespace PriceAggregator.Core.DataEntity
{
    [DataContract(Name = "ProductItemStatus")]
    public enum ProductItemStatus
    {
        [EnumMember] Disabled = 0,
        [EnumMember] Enabled = 1
    }
}