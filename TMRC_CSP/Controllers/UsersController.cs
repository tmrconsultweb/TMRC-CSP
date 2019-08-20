using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMRC_CSP.Logics;

namespace TMRC_CSP.Controllers
{
    [AuthorizationFilter(Roles = UserRoles.Partner)]
    public class UsersController : Controller
    {
        

        public ActionResult UsersAccount()
        {
            IExplorerProvider provider=null;
            ViewModel.Users.Users users = new ViewModel.Users.Users(provider);
            //IEnumerable<Microsoft.Store.PartnerCenter.Models.Customers.Customer> obj =
            users.Get("c1bf1bc9-7e27-4c17-9012-3164bd408264");
            return View("Admin/Users");
        }
    }
}