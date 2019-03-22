using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tcg.Owin.Cookies.SessionStore.Memory;
using Tcg.Owin.Security.OpenIdConnect;

[assembly: OwinStartup(typeof(WebApplication1.Startup))]

namespace WebApplication1
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            //Add TLS 1.2 to ServicePointManager.SecurityProtocol
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;

            app.UseTcgOpenIdConnectAuthentication(new TcgOpenIdConnectAuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings["Sts.ClientId"],
                ClientSecret = ConfigurationManager.AppSettings["Sts.ClientSecret"],
                ResponseType = "id_token token",
                Authority = "https://sts.competence.biz/identity",
                Scope = "openid name email idString tcg_claims api_tcg_claims",
                RedirectUri = ConfigurationManager.AppSettings["Sts.RedirectUri"],
                PostLogoutRedirectUri = ConfigurationManager.AppSettings["Sts.PostLogoutRedirectUri"],
                SessionStore = new MemoryAuthenticationSessionStore(),
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                Notifications = new TcgOpenIdConnectAuthenticationNotifications
                {
                    RedirectToIdentityProvider = (n) => {
                        return Task.FromResult(0);
                    }
                }
                // or
                //SessionStore = new RedisAuthenticationSessionStore(<redis connection string>)
            });
        }
    }
}