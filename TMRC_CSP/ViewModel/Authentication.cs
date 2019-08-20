using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMRC_CSP.ViewModel
{
    public class IsAuthenticated : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Request.IsAuthenticated == false)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    HttpContext.Current.Response.Redirect("~/Account/SignOut");
                    //filterContext.HttpContext.Response.StatusCode = 302; //Found Redirection to another page. Here- login page. Check Layout ajaxError() script.
                    //filterContext.HttpContext.Response.End();
                }
                else
                {
                    HttpContext.Current.Response.Redirect("~/Account/SignOut");
                    ////filterContext.Result = new RedirectResult(System.Web.Security.FormsAuthentication.LoginUrl + "?ReturnUrl=" +
                    ////     filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl));
                }
            }
            else
            {
                //Code HERE for page level authorization

            }
        }
    }
}