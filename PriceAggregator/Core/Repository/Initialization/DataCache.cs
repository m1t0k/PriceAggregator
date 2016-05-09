using System.Threading.Tasks;
using System.Web.Http;
using PriceAggregator.Core.Repository.Interfaces;

namespace PriceAggregator.Core.Repository.Initialization
{
    public class DataCache
    {
        public static void InitializeOnStartup()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;

            var repositoryList = new[]
            {typeof (IBrandRepository), typeof (ICategoryRepository), typeof (IRetailerRepository)};

            Parallel.ForEach(repositoryList,
                () => config.DependencyResolver.BeginScope(),
                (repository, loopState, scope) =>
                {
                    var instance = (IDataCache) scope.GetService(repository);
                    instance.PopulateCache();
                    return scope;
                },
                scope => scope.Dispose());
        }
    }
}