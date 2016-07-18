using System.Runtime.Serialization;

namespace PriceAggregator.Core.DataEntity
{
    [DataContract(Name = "PriceTaskStatus")]
    public enum PriceTaskStatus
    {
        [EnumMember] Scheduled = 1,
        [EnumMember] InProgress = 2,
        [EnumMember] Updated = 3,
        [EnumMember] Pending = 4,
        [EnumMember] Failed = 5
    }
}