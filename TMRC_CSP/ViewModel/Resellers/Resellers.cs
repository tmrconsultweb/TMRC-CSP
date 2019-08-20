using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMRC_CSP.Models;

namespace TMRC_CSP.ViewModel.Resellers
{
    public class Resellers
    {
        public IEnumerable<Models.Reseller> Getlist()
        {
            var db = new ViewModel.Context.ConnectionStringsContext();
            return db.Resellers.ToList();
        }

        public IEnumerable<Models.Reseller> GetActive()
        {
            var db = new ViewModel.Context.ConnectionStringsContext();
            return db.Resellers.Where(m => m.Status == true).ToList();
        }

        public string Save(Reseller r)
        {
            string msg = string.Empty;
            try
            {
                var db = new ViewModel.Context.ConnectionStringsContext();
                if (r.Id != 0)   //update
                {
                    var reseller = db.Resellers.Where(m => m.Id == r.Id).SingleOrDefault();
                    reseller.Address = r.Address;
                    reseller.City = r.City;
                    reseller.Country = r.Country;
                    reseller.Email = r.Email;
                    reseller.FirstName = r.FirstName;
                    reseller.LastName = r.LastName;
                    reseller.PhoneNumber = r.PhoneNumber;
                    reseller.ZipCode = r.ZipCode;
                    reseller.Margin = r.Margin;
                    msg = "Successfullt Reseller has been updated.";
                }
                else  //Insert
                {
                    if (db.Resellers.Any(m => m.Email == r.Email))
                        msg= "Email already exist try using another email.";
                   string pass = GeneratePassword.GeneratePassword.AutomaticGeneratePassword(12);
                    Models.Reseller reseller = new Models.Reseller
                    {
                        Address = r.Address,
                        ZipCode = r.ZipCode,
                        Status = true,
                        PhoneNumber = r.PhoneNumber,
                        City = r.City,
                        Country = r.Country,
                        CreatedDate = DateTime.Now,
                        Email = r.Email,
                        FirstName = r.FirstName,
                        LastName = r.LastName,
                        Password = pass,
                        Is1stTimePassChg = false,
                        Margin =r.Margin
                    };
                    db.Resellers.Add(reseller);
                    msg = "Successfully Reseller has been added.";
                    // string Emailbody = r.FirstName + ", " + r.LastName + " now you are our partner.<br /> Here is your password= " + r.Password + "  <br /> Please <a href='http://" + HttpContext.Current.Request.Url.Authority + "/Reseller/Account/Login/'> Login</a> and change your password";
                    //TMRC_CSP.ViewModel.SendEmail.SendEmailRegister(r.Email, "TMRC Partner", Emailbody);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                msg = "Unknown error occur, Please try again.";
            }
            return msg;
        }

        public Models.Reseller GetById(int? id)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                Models.Reseller reseller = db.Resellers.Where(m => m.Id == id).SingleOrDefault();
                return reseller;
            }
            catch (Exception ex)
            {
                return new Models.Reseller();
            }
        }

        public string Delete(int id, bool IsDelete)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                if (db.Resellers.Any(m => m.Id == id))
                {
                    db.Resellers.Where(m => m.Id == id).SingleOrDefault().Status = IsDelete ? false : true;
                    db.SaveChanges();
                }
                return IsDelete ? "Deleted" : "Activate";
            }
            catch (Exception ex)
            {
                return "Unknown error occur, Please try again.";
            }
        }


    }
}