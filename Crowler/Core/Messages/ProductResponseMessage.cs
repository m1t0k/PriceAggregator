using System;

namespace Crowler.Core.Messages
{
    public class ProductResponseMessage : BaseMessage
    {
        public ProductResponseMessage(long id, float? price, DateTime responsedAt, ProductResponseStatus status)
        {
            Id = id;
            Price = price;
            ResponsedAt = responsedAt;
            Status = status;
        }

        public long Id { get; private set; }
        public float? Price { get; private set; }
        public DateTime ResponsedAt { get; private set; }
        public ProductResponseStatus Status { get; private set; }
    }
}