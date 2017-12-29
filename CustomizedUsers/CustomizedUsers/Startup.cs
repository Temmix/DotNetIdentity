using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomizedUsers.Startup))]
namespace CustomizedUsers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
