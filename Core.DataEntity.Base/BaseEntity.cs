using System;

namespace PriceAggregator.Core.DataEntity.Base
{
    public class BaseEntity : IEquatable<BaseEntity>, IComparable<BaseEntity>
    {
        public int Id { get; set; }

        public  virtual int CompareTo(BaseEntity other)
        {
            return other != null ? Id.CompareTo(other.Id) : 1;
        }

        public virtual bool Equals(BaseEntity other)
        {
            return other != null && Id == other.Id;
        }
    }
}