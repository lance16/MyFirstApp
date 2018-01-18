using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RevalsysSystemTask.Startup))]
namespace RevalsysSystemTask
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
