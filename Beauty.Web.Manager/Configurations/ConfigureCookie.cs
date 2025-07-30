using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace Beauty.Web.Manager.Configurations
{
    public class ConfigureCookie : IConfigureNamedOptions<CookieAuthenticationOptions>
    {
        // You can inject services here
        public ConfigureCookie()
        {
        }

        public void Configure(string name, CookieAuthenticationOptions options)
        {
            //// Only configure the schemes you want
            //if (name == Startup.CookieScheme)
            //{
            //    // options.LoginPath = "/someotherpath";
            //}
        }

        public void Configure(CookieAuthenticationOptions options) => Configure(Options.DefaultName, options);
    }
}
