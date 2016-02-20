using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(LearningTogether.Web.Startup))]

namespace LearningTogether.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
