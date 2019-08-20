using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Areas.Reseller.ViewModel.TermConditions
{
    public class TermAndConditions : TMRC_CSP.ViewModel.TermConditions.TermConditions
    {
        public override TMRC_CSP.Models.TermsConditions GetTermsAndConditions()
        {
            var db = new TMRC_CSP.ViewModel.Context.ConnectionStringsContext();
            return db.TermsConditions.Where(m => m.Role == (int)TMRC_CSP.Models.Roles.Users && m.ResellerId == ViewModel.Account.Login._r.Id).SingleOrDefault();
        }

        public override string AddTermAndConditions(TMRC_CSP.Models.TermsConditions tc)
        {
            tc.Role = (int)TMRC_CSP.Models.Roles.Users;
            tc.ResellerId = ViewModel.Account.Login._r.Id;
            return base.AddTermAndConditions(tc);
        }

        public override string EditTermsAndConditions(TMRC_CSP.Models.TermsConditions tc)
        {
            return base.EditTermsAndConditions(tc);
        }

        public override string CheckIfExist(TMRC_CSP.Models.TermsConditions tc, int ResellerId=0)
        {
            tc.Role = (int)TMRC_CSP.Models.Roles.Users;
            ResellerId = Account.Login._r.Id;
            return base.CheckIfExist(tc, Account.Login._r.Id);
        }
    }
}