using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ComProgramPortal.Startup))]
namespace ComProgramPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
