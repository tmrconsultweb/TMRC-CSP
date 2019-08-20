using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMRC_CSP.Models;

namespace TMRC_CSP.Areas.Reseller.ViewModel.CustomerPrice
{
    public class CustomerPrice
    {


        //For Reseller Only
        public void Save(int ResellerId, GridPriceList priceList)
        {
            try
            {
                var db = new TMRC_CSP.ViewModel.Context.ConnectionStringsContext();
                //if (db.CustomerPrice.Any(m => m.PriceId == priceList.Id && m.ResellerId == ResellerId && m.Status == true))
                //{
                //    var rcp = db.CustomerPrice.Where(m => m.PriceId == priceList.Id && m.ResellerId == ResellerId).SingleOrDefault();
                //    rcp.Price = priceList.CustomerPrice;
                //}
                //else
                //{
                TMRC_CSP.Models.CustomerPrice rcp = new TMRC_CSP.Models.CustomerPrice()
                {
                    PriceId = priceList.Id,
                    Price = priceList.CustomerPrice,
                    ResellerId = ResellerId,
                    Status = true,
                };
                db.CustomerPrice.Add(rcp);
                //}
                db.SaveChanges();
            }
            catch
            {

            }
        }

        //public string UpdateCustomerPrice(TMRC_CSP.Models.GridPriceList gpl)
        //{
        //    try
        //    {
        //        var db = new TMRC_CSP.ViewModel.Context.ConnectionStringsContext();

        //        if (db.ResellerCustomersPrice.Any(m => m.PriceId == gpl.Id && m.ResellerId == Account.Login._r.Id))
        //        {
        //            var rcp = db.ResellerCustomersPrice.Where(m => m.PriceId == gpl.Id && m.ResellerId == Account.Login._r.Id).SingleOrDefault();
        //            rcp.CustomerPrice = gpl.CustomerPrice;
        //        }
        //        else
        //        {
        //            Models.ResellerCustomersPrice rcp = new Models.ResellerCustomersPrice()
        //            {
        //                CustomerPrice = gpl.CustomerPrice,
        //                PriceId = gpl.Id,
        //                ResellerId = Account.Login._r.Id,
        //            };
        //            db.ResellerCustomersPrice.Add(rcp);
        //        }
        //        db.SaveChanges();
        //        return "Successfully Saved";
        //    }
        //    catch(Exception ex)
        //    {
        //        return "Unknown error occur, Please try again.";
        //    }
        //}

        //For Admin Only
        public string UpdateCustomerAndResellerPrice(TMRC_CSP.Models.GridPriceList gpl)
        {
            try
            {
                var db = new TMRC_CSP.ViewModel.Context.ConnectionStringsContext();

                if (db.ResellerCustomersPrice.Any(m => m.PriceId == gpl.Id && m.ResellerId == 0))
                {
                    var rcp = db.ResellerCustomersPrice.Where(m => m.PriceId == gpl.Id && m.ResellerId == 0).SingleOrDefault();
                    rcp.CustomerPrice = gpl.CustomerPrice;
                    rcp.ResellerPrice = gpl.ResellerPrice;
                }
                else
                {
                    Models.ResellerCustomersPrice rcp = new Models.ResellerCustomersPrice()
                    {
                        CustomerPrice = gpl.CustomerPrice,
                        PriceId = gpl.Id,
                        ResellerPrice = gpl.ResellerPrice,
                        ResellerId = 0,
                    };
                    db.ResellerCustomersPrice.Add(rcp);
                }
                db.SaveChanges();
                return "Successfully Saved";
            }
            catch (Exception ex)
            {
                return "Unknown error occur, Please try again.";
            }
        }
    }
}