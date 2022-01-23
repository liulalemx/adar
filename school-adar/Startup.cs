using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(school_adar.Startup))]
namespace school_adar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
