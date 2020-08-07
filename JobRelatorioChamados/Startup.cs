using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobRelatorioChamados.Startup))]
namespace JobRelatorioChamados
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
