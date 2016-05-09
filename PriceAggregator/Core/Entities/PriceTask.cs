﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PriceAggregator.Core.Entities
{
    [DataContract(Name = "pricetask")]
    public class PriceTask : BaseEntity
    {
        [DataMember(Name = "prid")]
        public string ProductItemId { get; set; }

        [DataMember(Name = "lastupd")]
        public DateTime? LastUpdated { get; set; }

        [DataMember(Name = "status")]
        public PriceTaskStatus Status { get; set; }
    }


    [DataContract(Name = "pricetasks")]
    public class PriceTaskCollection
    {
        [DataMember(Name = "items")]
        public List<PriceTask> Items { get; set; }
    }
}