using System;

namespace Crowler.Core.Messages
{
    public class ProductRequestMessage : BaseMessage
    {
        public ProductRequestMessage(long id,long productId,long retailerId,DateTime requestedAt, string url)
        {
            Id = id;
            ProductId = productId;
            RetailerId = retailerId;
            RequestedAt = requestedAt;
            Url = url;
        }

        public long Id { get; private set; }
        public long ProductId { get; private set; }
        public long RetailerId { get; private set; }
        public DateTime RequestedAt { get; private set; }
        public string Url { get; private set; }
    }
}