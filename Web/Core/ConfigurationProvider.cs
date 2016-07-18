using System.Configuration;

namespace PriceAggregator.Web.Core
{
    public static class ConfigurationProvider
    {
        public static string DictionaryApiUrl => ConfigurationManager.AppSettings["DictionaryApiUrl"];
    }
}