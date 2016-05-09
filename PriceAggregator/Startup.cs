using Microsoft.Owin;
using Owin;
using PriceAggregator;

[assembly: OwinStartup(typeof (Startup))]

namespace PriceAggregator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}