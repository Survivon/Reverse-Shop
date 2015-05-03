using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Reverse_Shop.Startup))]
namespace Reverse_Shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
