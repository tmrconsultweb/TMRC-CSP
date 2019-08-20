using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.ViewModel.DefaultMargin
{
    public class DefaultMarginUsers : DefaultMargin
    {
        public override Models.DefaultMargin GetDefaultMargin()
        {
            var db = new Context.ConnectionStringsContext();
            return db.DefaultMargin.Where(m => m.Role == (int)Models.Roles.Users && m.ResellerId == 0).SingleOrDefault();
        }

        public override string AddDefaultMargin(Models.DefaultMargin d)
        {
            d.Role = (int)Models.Roles.Users;
            d.ResellerId = 0;
            return base.AddDefaultMargin(d);
        }

        public override string EditDefaultMargin(Models.DefaultMargin d)
        {
            return base.EditDefaultMargin(d);
        }

        public override string CheckIfExist(Models.DefaultMargin d, int ResellerId=0)
        {
            d.Role = (int)Models.Roles.Users;
            return base.CheckIfExist(d);
        }
    }
}