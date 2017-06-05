using Constants;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Infrastructure;
using Owin;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AuthorizationServer {
    public partial class Startup {
        public void ConfigAuth(IAppBuilder app) {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions {
                AuthenticationType = "Application",
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Passive,
                LoginPath = new Microsoft.Owin.PathString(Paths.LoginPath),
                LogoutPath = new Microsoft.Owin.PathString(Paths.LogoutPath)
            });

            app.SetDefaultSignInAsAuthenticationType("External");
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions {
                AuthenticationType = "External",
                AuthenticationMode = AuthenticationMode.Passive,
                CookieName = CookieAuthenticationDefaults.CookiePrefix + "External",
                ExpireTimeSpan = TimeSpan.FromMinutes(5)
            });

            app.UseGoogleAuthentication("", "");

            app.UseOAuthAuthorizationServer(new Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerOptions {
                AuthorizeEndpointPath = new Microsoft.Owin.PathString(Paths.AuthorizePath),
                TokenEndpointPath = new Microsoft.Owin.PathString(Paths.TokenPath),
                ApplicationCanDisplayErrors = true,

                AllowInsecureHttp = true,

                Provider = new Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerProvider {
                    OnValidateClientRedirectUri = ValidateClientRedirectUri,
                    OnValidateClientAuthentication = ValidateClientAuthentication
                },

                AuthorizationCodeProvider = new Microsoft.Owin.Security.Infrastructure.AuthenticationTokenProvider { },

                RefreshTokenProvider = new Microsoft.Owin.Security.Infrastructure.AuthenticationTokenProvider { }
            });
        }

        private Task ValidateClientRedirectUri(Microsoft.Owin.Security.OAuth.OAuthValidateClientRedirectUriContext context) {
            return Task.FromResult(0);
        }
        private Task ValidateClientAuthentication(Microsoft.Owin.Security.OAuth.OAuthValidateClientAuthenticationContext context) {
            return Task.FromResult(0);
        }
        private readonly ConcurrentDictionary<string, string> _authenticationCodes = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);
        private void CreateAuthenticationCode(Microsoft.Owin.Security.Infrastructure.AuthenticationTokenCreateContext context) {
            context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
            _authenticationCodes[context.Token] = context.SerializeTicket();
        }
        private void ReceiveAuthenticationCode(AuthenticationTokenReceiveContext context) {
            if()
        }
    }
}