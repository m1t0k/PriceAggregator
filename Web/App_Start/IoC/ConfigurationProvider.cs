using System.Configuration;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Web.BusinessLogic.Helpers;

namespace PriceAggregator.Web.IoC
{
    public static class ConfigurationProvider
    {
        public static string DictionaryApiUrl => ConfigurationManager.AppSettings["DictionaryApiUrl"];
        public static string DictionaryApiKey => ConfigurationManager.AppSettings["DictionaryApiKey"];

        public static IDictionaryRestClient CreateDictionaryRestClient(ILoggingService logger)
        {
            return new DictionaryRestClient(DictionaryApiUrl, DictionaryApiKey, logger);
        }
    }
}