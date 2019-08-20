using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMRC_CSP.Models;

namespace TMRC_CSP.ViewModel.TermConditions
{
    public class TermConditionsResellers : TermConditions
    {
        public override Models.TermsConditions GetTermsAndConditions()
        {
            var db = new Context.ConnectionStringsContext();
            return db.TermsConditions.Where(m => m.Role == (int)Models.Roles.Resellers && m.ResellerId == 0).SingleOrDefault();
        }

        public override string AddTermAndConditions(TermsConditions tc)
        {
            tc.Role = (int)Roles.Resellers;
            tc.ResellerId = 0;
            return base.AddTermAndConditions(tc);
        }

        public override string EditTermsAndConditions(TermsConditions tc)
        {
            return base.EditTermsAndConditions(tc);
        }

        public override string CheckIfExist(TermsConditions tc, int ResellerId=0)
        {
            tc.Role = (int)Models.Roles.Resellers;
            return base.CheckIfExist(tc);
        }
    }
}