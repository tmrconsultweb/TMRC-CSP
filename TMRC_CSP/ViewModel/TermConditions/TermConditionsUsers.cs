using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.ViewModel.TermConditions
{
    public class TermConditionsUsers : TermConditions
    {
        public override Models.TermsConditions GetTermsAndConditions()
        {
            var db = new Context.ConnectionStringsContext();
            return db.TermsConditions.Where(m=>m.Role == (int)Models.Roles.Users && m.ResellerId == 0).SingleOrDefault();
        }

        public override string AddTermAndConditions(Models.TermsConditions tc)
        {
            tc.Role = (int)Models.Roles.Users;
            tc.ResellerId = 0;
            return base.AddTermAndConditions(tc);
        }

        public override string EditTermsAndConditions(Models.TermsConditions tc)
        {
            return base.EditTermsAndConditions(tc);
        }

        public override string CheckIfExist(Models.TermsConditions tc, int ResellerId=0)
        {
            tc.Role = (int)Models.Roles.Users;
            return base.CheckIfExist(tc);
        }
    }
}