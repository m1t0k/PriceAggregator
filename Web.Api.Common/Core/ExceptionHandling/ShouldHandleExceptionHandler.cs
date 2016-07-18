using System;
using PriceAggregator.Core.Exceptions;

namespace Web.Api.Common.Core.ExceptionHandling
{
    public static class ShouldHandleExceptionHandler
    {
        public static bool ShouldHandle(Exception ex)
        {
            return !(ex is ObjectAlreadyExistsException) && !(ex is ObjectDoesNotExistException);
        }
    }
}