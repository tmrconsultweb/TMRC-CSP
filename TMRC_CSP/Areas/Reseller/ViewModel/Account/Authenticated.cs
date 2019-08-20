using System.Web;
using System.Web.Mvc;


namespace TMRC_CSP.Areas.Reseller.ViewModel.Account
{
    public class Authenticated : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Login._r == null)  //it's authenticated person/Reseller
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    HttpContext.Current.Response.Redirect("../Account/SignOut");
                    //filterContext.HttpContext.Response.StatusCode = 302; //Found Redirection to another page. Here- login page. Check Layout ajaxError() script.
                    //filterContext.HttpContext.Response.End();
                }
                else
                {
                    HttpContext.Current.Response.Redirect("../Account/SignOut");
                    ////filterContext.Result = new RedirectResult(System.Web.Security.FormsAuthentication.LoginUrl + "?ReturnUrl=" +
                    ////     filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl));
                }
            }
            else
            {
                if (Login._r.Is1stTimePassChg == false && !HttpContext.Current.Request.Url.ToString().Contains("/ChangePassword"))
                {
                    HttpContext.Current.Response.Redirect("../Home/ChangePassword");
                }
                //Code HERE for page level authorization

            }
        }
    }
}