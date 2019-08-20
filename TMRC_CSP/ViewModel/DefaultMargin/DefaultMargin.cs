using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMRC_CSP.Models;

namespace TMRC_CSP.ViewModel.DefaultMargin
{
    public abstract class DefaultMargin
    {
        public abstract Models.DefaultMargin GetDefaultMargin();

        public virtual string AddDefaultMargin(Models.DefaultMargin d)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                db.DefaultMargin.Add(d);
                db.SaveChanges();
                return "Successfully Saved";
            }
            catch (Exception ex)
            {
                return "Unknown error occur, Please try again.";
            }
        }

        public virtual string EditDefaultMargin(Models.DefaultMargin d)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                Models.DefaultMargin defaultMargin = db.DefaultMargin.Where(m => m.Id == d.Id).SingleOrDefault();
                defaultMargin.DefaultPercentage = d.DefaultPercentage;
                db.SaveChanges();
                return "Successfully Updated";
            }
            catch (Exception ex)
            {
                return "Unknown error occur, Please try again.";
            }
        }

        public virtual string CheckIfExist(Models.DefaultMargin d, int ResellerId=0)
        {
            d.CreatedDate = DateTime.Now;
            var db = new Context.ConnectionStringsContext();
            if (db.DefaultMargin.Any(m => m.Role == (int)d.Role && m.ResellerId == ResellerId)) //Update
            {
                return EditDefaultMargin(d);
            }
            else  //Add
            {
                return AddDefaultMargin(d);
            }
        }
    }
}