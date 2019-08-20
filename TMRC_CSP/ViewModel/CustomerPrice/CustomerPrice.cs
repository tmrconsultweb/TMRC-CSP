using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMRC_CSP.Models;

namespace TMRC_CSP.ViewModel.CustomerPrice
{
    public class CustomerPrice
    {
        public void Save(int Id, GridPriceList priceList)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                if (db.CustomerPrice.Any(m => m.PriceId == priceList.Id && m.ResellerId == Id && m.Status == true))
                {
                    var rcp = db.CustomerPrice.Where(m => m.PriceId == priceList.Id && m.ResellerId == Id).SingleOrDefault();
                    rcp.Price = priceList.ResellerPrice;
                }
                else
                {
                    Models.CustomerPrice rcp = new Models.CustomerPrice()
                    {
                        PriceId = priceList.Id,
                        Price = priceList.ResellerPrice,
                        ResellerId = Id,
                        Status = true,
                    };
                    db.CustomerPrice.Add(rcp);
                }
                db.SaveChanges();
            }
            catch
            {

            }
        }

    }
}