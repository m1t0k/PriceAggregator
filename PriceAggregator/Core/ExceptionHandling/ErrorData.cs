using System;

namespace PriceAggregator.Core.ExceptionHandling
{
    public class ErrorData
    {
        public string ErrorMessage { get; set; }
        public DateTime ErrorDateTime { get; set; }
        public Uri RequestedUri { get; set; }
    }
}