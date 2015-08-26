using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineBookReader.Startup))]
namespace OnlineBookReader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
