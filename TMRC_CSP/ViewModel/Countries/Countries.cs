using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TMRC_CSP.ViewModel.Countries
{
    public class Countries
    {
        public object GetCountries()
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                return db.Countries.Where(m => m.Status == true).ToList();
                //return (from c in db.Countries
                //        where c.Status == true
                //        select new { Text = c.CountryName, Value = c.Code }).ToList();
            }
            catch
            {
                throw new Exception("Unknown error occur, Please try again.");
            }

        }
    }
}