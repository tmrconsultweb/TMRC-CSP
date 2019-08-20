using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using TMRC_CSP.Models;

namespace TMRC_CSP.ViewModel.PromotionCodes
{
    public class PromotionCodes
    {
        public List<Models.PromotionCodes> GetList()
        {
            var db = new Context.ConnectionStringsContext();
            return db.PromotionCodes.Where(m => m.Status == true).ToList();
        }

        public string SavePromoCode(Models.PromotionCodes p)
        {
            string r = string.Empty;
            try
            {
                var db = new Context.ConnectionStringsContext();
                if (db.PromotionCodes.Any(m => m.Id == p.Id))  //update
                {
                    var pro = db.PromotionCodes.Where(m => m.Id == p.Id).SingleOrDefault();
                    pro.ExpiryDate = p.ExpiryDate;
                    pro.Amount = p.Amount;
                    r = "Successfully Update";
                }
                else  //Add
                {
                    Models.PromotionCodes promotionCodes = new Models.PromotionCodes()
                    {
                        Code = p.Code,
                        CreatedDate = DateTime.Now,
                        ExpiryDate = p.ExpiryDate,
                        IsApplied = false,
                        Status = true,
                        Amount=p.Amount,
                    };
                    db.PromotionCodes.Add(promotionCodes);
                    r = "Successfully saved";
                }
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                r= "Unknown error occur, Please try again.";
            }
            return r;
        }

        public void DeletePromoCode(int id)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                db.PromotionCodes.Where(m => m.Id == id).SingleOrDefault().Status = false;
                db.SaveChanges();
            }
            catch
            {

            }
        }

        public Models.PromotionCodes GetPromoCodeById(int? id)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                return db.PromotionCodes.Where(m => m.Id == id).SingleOrDefault();
            }   
            catch(Exception ex)
            {
                return new Models.PromotionCodes();
            }
        }

        public Models.PromotionCodes GetPromoCodeByCoode(string code)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                return db.PromotionCodes.Where(m => m.Code == code && m.IsApplied == false && m.Status == true && DbFunctions.TruncateTime(m.ExpiryDate).Value >= DateTime.Now).SingleOrDefault();
                //return (from p in db.PromotionCodes
                //        where p.Code == code && p.IsApplied ==false 
                //        && p.Status == true
                //        && DbFunctions.TruncateTime(o.Date).Value >= PreviusDate.Date)
            }
            catch (Exception ex)
            {
                return new Models.PromotionCodes();
            }
        }
    }
}