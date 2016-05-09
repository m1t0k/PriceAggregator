using System.Web.Mvc;
using PriceAggregator.Core.ExceptionHandling;
using PriceAggregator.Core.Filters;

namespace PriceAggregator
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new ValidateModelFilter());
        }
    }
}