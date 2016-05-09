using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;
using PriceAggregator.Core.Entities;

namespace PriceAggregator.Core.ExceptionHandling
{
    public class PriceTaskStatusConstraint : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
            IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;
            if (!values.TryGetValue(parameterName, out value) || value == null) return false;
            var stringValue = value as string;
            if (stringValue == null) return false;
            PriceTaskStatus status;
            return Enum.TryParse(stringValue, true, out status);
        }
    }
}