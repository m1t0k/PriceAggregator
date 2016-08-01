using Microsoft.Owin;
using Owin;
using PriceAggregator.Web;

[assembly: OwinStartup(typeof (Startup))]

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