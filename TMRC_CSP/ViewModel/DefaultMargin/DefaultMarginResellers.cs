using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMRC_CSP.Models;

namespace TMRC_CSP.ViewModel.DefaultMargin
{
    public class DefaultMarginResellers : DefaultMargin
    {
        public override Models.DefaultMargin GetDefaultMargin()
        {
            var db = new Context.ConnectionStringsContext();
            return db.DefaultMargin.Where(m => m.Role == (int)Models.Roles.Resellers).SingleOrDefault();
        }

        public override string AddDefaultMargin(Models.DefaultMargin d)
        {
            d.Role = (int)Roles.Resellers;
            d.ResellerId = 0;
            return base.AddDefaultMargin(d);
        }

        public override string EditDefaultMargin(Models.DefaultMargin d)
        {
            return base.EditDefaultMargin(d);
        }

        public override string CheckIfExist(Models.DefaultMargin d, int ResellerId=0)
        {
            d.Role = (int)Models.Roles.Resellers;
            return base.CheckIfExist(d);
        }
    }
}