using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

namespace TMRC_CSP.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Logs a user in to the application.
        /// </summary>
        public void Login()
        {
            string callbackUrl = Url.Action("Index", "Admin");
            if (!Request.IsAuthenticated)
            {
                
                HttpContext.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = callbackUrl },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
            else
            {
                Response.Redirect(callbackUrl);
            }
        }

        /// <summary>
        /// Logs a user out of the application.
        /// </summary>
        public void SignOut()
        {
            string callbackUrl = Url.Action("Index", "Home", routeValues: null, protocol: Request.Url.Scheme);

            HttpContext.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                OpenIdConnectAuthenticationDefaults.AuthenticationType,
                CookieAuthenticationDefaults.AuthenticationType);
        }
    }
}