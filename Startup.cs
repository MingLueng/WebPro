using Microsoft.Owin;
using Owin;
using System.Web.Services.Description;
using WebBanHangOnline.Models.Common;

[assembly: OwinStartupAttribute(typeof(WebBanHangOnline.Startup))]
namespace WebBanHangOnline
{
    public partial class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
          
            ConfigureAuth(app);
        }
    }
}
