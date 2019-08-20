using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.ViewModel.ResellerCustomers
{
    public class ResellerCustomers
    {
        public List<Models.Customers> Get(int ResellerId)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                return db.Customers.Where(m => m.Status == true && m.ResellerId == ResellerId).ToList();
            }
            catch
            {
                return new List<Models.Customers>();
            }
        }
    }
}