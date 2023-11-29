using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPrivateExnessCalculator.Startup))]
namespace MyPrivateExnessCalculator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
