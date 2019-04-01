using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Laboratorio_7_ArbolesB_MarceloRosales.Startup))]
namespace Laboratorio_7_ArbolesB_MarceloRosales
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
