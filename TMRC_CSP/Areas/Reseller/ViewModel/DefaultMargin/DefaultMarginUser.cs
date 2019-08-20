using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Areas.Reseller.ViewModel.DefaultMargin
{
    public class DefaultMarginUser : TMRC_CSP.ViewModel.DefaultMargin.DefaultMargin
    {
        public override TMRC_CSP.Models.DefaultMargin GetDefaultMargin()
        {
            var db = new TMRC_CSP.ViewModel.Context.ConnectionStringsContext();
            return db.DefaultMargin.Where(m => m.Role == (int)TMRC_CSP.Models.Roles.Users && m.ResellerId == Account.Login._r.Id).SingleOrDefault();
        }

        public override string AddDefaultMargin(TMRC_CSP.Models.DefaultMargin d)
        {
            d.Role = (int)TMRC_CSP.Models.Roles.Users;
            d.ResellerId = Account.Login._r.Id;
            return base.AddDefaultMargin(d);
        }

        public override string EditDefaultMargin(TMRC_CSP.Models.DefaultMargin d)
        {
            return base.EditDefaultMargin(d);
        }

        public override string CheckIfExist(TMRC_CSP.Models.DefaultMargin d, int ResellerId)
        {
            d.Role = (int)TMRC_CSP.Models.Roles.Users;
            return base.CheckIfExist(d, Account.Login._r.Id);
        }
    }
}