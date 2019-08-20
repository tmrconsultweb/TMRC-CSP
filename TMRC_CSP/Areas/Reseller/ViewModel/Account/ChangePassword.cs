using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Areas.Reseller.ViewModel.Account
{
    public class ChangePassword
    {
        public string SavePassword(Models.ChangePassword c)
        {
            try
            {
                var db = new TMRC_CSP.ViewModel.Context.ConnectionStringsContext();
                if (Login._r.Password == c.OldPassword) //Confirmation is it valid user
                {
                    var res = db.Resellers.Where(m => m.Email == Login._r.Email).SingleOrDefault();
                    res.Password = c.NewPassword;
                    res.Is1stTimePassChg = true;
                    db.SaveChanges();
                    ViewModel.Account.Login login = new ViewModel.Account.Login();
                    login.SignOut();
                    return "Successfully Password Changed.";
                }
                return "Unknown error ocuur, Please try again.";
            }
            catch(Exception ex)
            {
                return "Unknown error ocuur, Please try again.";
            }
        }
    }
}