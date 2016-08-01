using Microsoft.Owin;
using Owin;
using PriceAggregator.Web;
using PriceAggregator.Web.Core.IoC;
[assembly: OwinStartup(typeof(PriceAggregator.Web.Startup))]
namespace PriceAggregator.Web
{

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
            ConfigureAuth(app);
        }
    }
}
