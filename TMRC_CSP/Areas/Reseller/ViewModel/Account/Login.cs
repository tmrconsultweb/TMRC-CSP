using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Areas.Reseller.ViewModel.Account
{

    public class Login
    {
        public static TMRC_CSP.Models.Reseller _r
        {
            get
            {
                //store the object in session if not already stored
                if (HttpContext.Current.Session["Reseller"] == null || HttpContext.Current.Session["Reseller"] == "")
                {
                    if (HttpContext.Current.Request.IsLocal)
                    {
                        TMRC_CSP.Models.Reseller reseller = new TMRC_CSP.Models.Reseller
                        {
                            Id = 1,
                            Address = "habibi resturant I8 Markez",
                            City = "islamabad",
                            Country = "Pakistan",
                            CreatedDate = DateTime.Now,
                            Email = "ahsankamalkhan5@gmail.com",
                            FirstName = "Ahsan",
                            Is1stTimePassChg = true,
                            LastName = "Kamal",
                            Password = "Tmrc123",
                            PhoneNumber = "03469728288",
                            Status = true,
                            ZipCode = "45000"
                        };
                        return reseller;
                    }
                    return null;
                }
                //return the object from session
                return (TMRC_CSP.Models.Reseller)HttpContext.Current.Session["Reseller"];
            }
        }

        public void SignOut()
        {
            HttpContext.Current.Session.Remove("Reseller");
            bool gone = (HttpContext.Current.Session["Reseller"] == null);
            if (!gone)
                HttpContext.Current.Session.Clear();
        }

        public string IsValid(TMRC_CSP.Areas.Reseller.Models.ResellerLogin r)
        {
            try
            {
                if (r.Email != "" && r.Password != "")
                {
                    var db = new TMRC_CSP.ViewModel.Context.ConnectionStringsContext();
                    if (db.Resellers.Any(m => m.Email == r.Email && m.Password == r.Password))
                    {
                        TMRC_CSP.Models.Reseller res = db.Resellers.Where(m => m.Email == r.Email && m.Password == r.Password).SingleOrDefault();
                        HttpContext.Current.Session["Reseller"] = res;
                        if (res.Is1stTimePassChg == false)
                            return "../Home/ChangePassword";
                        return "../Home/Index";
                    }
                    else
                        return "Invalid login, Please try again.";
                }
                return "Invalid login, Please try again.";
            }
            catch (Exception ex)
            {
                return "Invalid login, Please try again.";
            }
        }
    }
}