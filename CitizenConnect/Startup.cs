using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CitizenConnect.Startup))]
namespace CitizenConnect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
