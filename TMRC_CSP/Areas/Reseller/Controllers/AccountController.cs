using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMRC_CSP.Areas.Reseller.Controllers
{
    public class AccountController : Controller
    {
        // GET: Reseller/Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TMRC_CSP.Areas.Reseller.Models.ResellerLogin r)
        {
            string res = string.Empty;
            if (ModelState.IsValid)
            {
                ViewModel.Account.Login login = new ViewModel.Account.Login();
                res = login.IsValid(r);
                if (res.Contains("Home/"))
                    return RedirectToAction(res);
            }
            ViewBag.msg = res;
            return View();
        }

        public ActionResult SignOut()
        {
            ViewModel.Account.Login login = new ViewModel.Account.Login();
            login.SignOut();
            return RedirectToAction("Login");
        }
    }
}