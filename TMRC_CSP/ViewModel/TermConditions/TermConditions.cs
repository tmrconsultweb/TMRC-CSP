using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.ViewModel.TermConditions
{
    public abstract class TermConditions
    {

        public abstract Models.TermsConditions GetTermsAndConditions();

        public virtual string AddTermAndConditions(Models.TermsConditions tc)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                db.TermsConditions.Add(tc);
                db.SaveChanges();
                return "Successfully Saved";
            }
            catch (Exception ex)
            {
                return "Unknown error occur, Please try again.";
            }
        }


        public virtual string EditTermsAndConditions(Models.TermsConditions tc)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                Models.TermsConditions termsConditions = db.TermsConditions.Where(m => m.Id == tc.Id).SingleOrDefault();
                termsConditions.Info = tc.Info;
                db.SaveChanges();
                return "Successfully Updated";
            }
            catch (Exception ex)
            {
                return "Unknown error occur, Please try again.";
            }
        }

        public virtual string CheckIfExist(Models.TermsConditions tc, int ResellerId=0)
        {
            tc.CreatedDate = DateTime.Now;
            var db = new Context.ConnectionStringsContext();
            if (db.TermsConditions.Any(m => m.Role == (int)tc.Role && m.ResellerId == ResellerId)) //Update
            {
                return EditTermsAndConditions(tc);
            }
            else  //Add
            {
                return AddTermAndConditions(tc);
            }
        }
    }
}