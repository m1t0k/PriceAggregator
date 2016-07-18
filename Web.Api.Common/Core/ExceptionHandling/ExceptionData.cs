using System;

namespace Web.Api.Common.Core.ExceptionHandling
{
    public class ExceptionData
    {
        public Exception Exception { get; set; }
        public DateTime ExceptionDateTime { get; set; }
        public Uri RequestedUri { get; set; }
    }
}