using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMRC_CSP.Models;

namespace TMRC_CSP.Areas.Reseller.ViewModel.Profile
{
    public class Profile
    {
        public string Save(TMRC_CSP.Models.Reseller r)
        {
            try
            {
                var db = new TMRC_CSP.ViewModel.Context.ConnectionStringsContext();
                var res = db.Resellers.Where(m => m.Email == Account.Login._r.Email && m.Password == Account.Login._r.Password).SingleOrDefault();
                res.FirstName = r.FirstName;
                res.LastName = r.LastName;
                res.Address = r.Address;
                db.SaveChanges();
                return "Successfully updated.";
            }
            catch (Exception ex)
            {
                return "Unknown error occur, Please try again.";
            }
        }
    }
}