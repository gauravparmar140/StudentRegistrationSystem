using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentRegistrationSystem.Startup))]
namespace StudentRegistrationSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //5ConfigureAuth(app);
        }
    }
}
