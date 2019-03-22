# Upgrading from global.asax to Owin / Startup

* Install OWIN (step already done in this project)
  ```
  Install-Package Microsoft.AspNet.WebApi.OwinSelfHost
  Install-Package Microsoft.Owin.Host.SystemWeb
  ```
* Install Tcg.Owin.Security (step already done in this project)
  ```
  Install-Package Tcg.Owin.Security.OpenIdConnect
  Install-Package Tcg.Owin.Cookies.SessionStore.Memory
  ```
  (Or install Tcg.Owin.Cookies.SessionStore.Redis) for use with a Redis instance
* Add class Startup (step already done in this project)
  ```
  public static class Startup 
    {
        public static void Configuration(IAppBuilder app)
        {
            //Add TLS 1.2 to ServicePointManager.SecurityProtocol
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;

            app.UseTcgOpenIdConnectAuthentication(new TcgOpenIdConnectAuthenticationOptions
            {
                ClientId = <clientId>,    
                ClientSecret = <clientSecret>,
                ResponseType = "id_token token",
                Authority = <authority>,
                Scope = <required scopes>,
                RedirectUri = <redirect url>,
                PostLogoutRedirectUri = <post logout redirect url>,
                SessionStore = new MemoryAuthenticationSessionStore()
                // or
                //SessionStore = new RedisAuthenticationSessionStore(<redis connection string>)
            });
        }
    }
  ```
* Configure ClientId and Secret in web.config.
* Remove Tcg.DomainSecurity.Module.DomSecModule from web.config
