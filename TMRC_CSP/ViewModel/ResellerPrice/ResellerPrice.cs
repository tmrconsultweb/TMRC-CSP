using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMRC_CSP.Models;

namespace TMRC_CSP.ViewModel.ResellerPrice
{
    public class ResellerPrice
    {
        public List<Models.GridPriceList> Get(int Id)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                var query = (from p in db.MicrosoftPriceList
                                 //join rp in db.ResellerPrice
                                 //on p.MicrosoftId equals rp.MicrosoftId
                                 //into rps
                                 //from rp in rps.DefaultIfEmpty()
                             where p.Status == true //&& rp.ResellerId == Id
                             group p by p.MicrosoftId into op
                             select new
                             {
                                 MicrosoftId = op.Key,
                                 Name = op.Max(x => x.Name),
                                 StartDate = op.Max(x => x.StartDate),
                                 EndDate = op.Max(x => x.EndDate),
                                 Price = op.Max(x => x.Price),
                                 ResellerPrice = db.ResellerPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == Id && m.Status == true)
                                                ? db.ResellerPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == Id && m.Status == true).OrderByDescending(m=>m.Id).FirstOrDefault().Price
                                                : (db.ResellerCustomersPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0 && m.ResellerPrice != null))
                                                ? db.ResellerCustomersPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0).OrderByDescending(m => m.Id).FirstOrDefault().ResellerPrice
                                                : 0,
                                 CustomerPrice = (db.ResellerCustomersPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0 && m.CustomerPrice != null))
                                                ? db.ResellerCustomersPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0).OrderByDescending(m => m.Id).FirstOrDefault().CustomerPrice
                                                : op.Max(x => x.CustomerPrice),
                                 AgreementType = op.Max(x => x.AgreementType),
                                 CustomerType = op.Max(x => x.CustomerType),
                                 LicenseType = op.Max(x => x.LicenseType),
                                 PurchaseUnit = op.Max(x => x.PurchaseUnit),
                                 PurchaseUnitNumber = op.Max(x => x.PurchaseUnitNumber),
                                 Id = op.Max(x => x.Id),
                                 DefaultMarginReseller = db.Resellers.Where(m => m.Id == Id).FirstOrDefault().Margin   //get the margin from reseller table
                             }).ToList();



                List<Models.GridPriceList> mic = query.ToList().Select(r => new Models.GridPriceList
                {
                    EndDate = r.EndDate,
                    Id = r.Id,
                    MicrosoftId = r.MicrosoftId,
                    Name = r.Name,
                    StartDate = r.StartDate,
                    // Date = r.StartDate.Date + "<br/> "+r.EndDate.Date,
                    Price = r.Price,
                    AgreementType = AgreementTypes.ParseEnumToString<AgreementType>(r.AgreementType),
                    CustomerType = CustomerTypes.ParseEnumToString<CustomerType>(r.CustomerType),
                    LicenseType = LicenseTypes.ParseEnumToString<LicenseType>(r.LicenseType),
                    ResellerPrice = (double)r.ResellerPrice != 0 ? (double)r.ResellerPrice : (r.Price + ((r.CustomerPrice - r.Price) * r.DefaultMarginReseller) / 100),
                    ResellerPricePercentage = ((double)System.Math.Round(((double)((((double)r.ResellerPrice != 0 ?
                    (double)r.ResellerPrice
                    : (r.Price + ((r.CustomerPrice - r.Price) * r.DefaultMarginReseller) / 100)) - r.Price) * 100) / r.Price), 2)).ToString(),
                    //((double)System.Math.Round(((double)((((double)r.ResellerPrice != 0 ? (double)r.ResellerPrice : (r.CustomerPrice - (r.CustomerPrice * r.DefaultMarginReseller) / 100)) - r.Price) * 100) / r.Price), 2)).ToString(),
                    CustomerPrice = r.CustomerPrice,
                    CustomerPricePercentage = ((double)System.Math.Round((((r.CustomerPrice - r.Price) * 100) / r.Price), 2)).ToString(),
                    PurchaseUnit = r.PurchaseUnitNumber + " " + PurchaseUnits.ParseEnumToString<PurchaseUnit>(r.PurchaseUnit),
                    //PurchaseUnitNumber = r.PurchaseUnitNumber,
                    DefaultMarginReseller = (double)r.ResellerPrice != 0 ?

                   ((double)((

                   ((double)System.Math.Round((((r.CustomerPrice - r.Price) * 100) / r.Price), 2))
                   -
                    ((double)System.Math.Round(((double)((((double)r.ResellerPrice != 0 ?
                    (double)r.ResellerPrice
                    : (r.Price + ((r.CustomerPrice - r.Price) * r.DefaultMarginReseller) / 100)) - r.Price) * 100) / r.Price), 2)))
                    * 100
                    )
                / ((double)(((double)System.Math.Round((((r.CustomerPrice - r.Price) * 100) / r.Price), 2))))
                    ).ToString()
                    : r.DefaultMarginReseller.ToString()
                }).OrderBy(m => m.Name).ToList();

                return mic;
            }
            catch
            {
                return new List<Models.GridPriceList>();
            }
        }


        public void UpdateResellerPrice(int Id, GridPriceList priceList)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                //if (db.ResellerPrice.Any(m => m.PriceId == priceList.Id && m.ResellerId == Id && m.Status == true))
                //{
                //    var rcp = db.ResellerPrice.Where(m => m.PriceId == priceList.Id && m.ResellerId == Id).SingleOrDefault();
                //    rcp.Price = priceList.ResellerPrice;
                //}
                //else
                //{
                Models.ResellerPrice rcp = new Models.ResellerPrice()
                {
                    PriceId = priceList.Id,
                    Price = priceList.ResellerPrice,
                    ResellerId = Id,
                    Status = true,
                };
                db.ResellerPrice.Add(rcp);
                //}
                db.SaveChanges();
            }
            catch
            {

            }
        }


        public string Reset(int Id)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                db.ResellerPrice.Where(m => m.Status == true && m.ResellerId == Id).ToList().ForEach(c => c.Status = false);
                db.SaveChanges();
                return "Reset";
            }
            catch
            {
                return "Error";
            }
        }

        //For Reseller
        public List<Models.GridPriceList> GetForReseller(int Id)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                var query = (from p in db.MicrosoftPriceList
                                 //join rp in db.ResellerPrice
                                 //on p.MicrosoftId equals rp.MicrosoftId
                                 //into rps
                                 //from rp in rps.DefaultIfEmpty()
                             where p.Status == true //&& rp.ResellerId == Id
                             group p by p.MicrosoftId into op
                             select new
                             {
                                 MicrosoftId = op.Key,
                                 Name = op.Max(x => x.Name),
                                 StartDate = op.Max(x => x.StartDate),
                                 EndDate = op.Max(x => x.EndDate),
                                 Price = op.Max(x => x.Price),
                                 ResellerPrice = db.ResellerPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == Id && m.Status == true) ?
                                                 db.ResellerPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == Id && m.Status == true).OrderByDescending(m => m.Id).FirstOrDefault().Price :
                                                (db.ResellerCustomersPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0 && m.ResellerPrice != null))
                                                 ? db.ResellerCustomersPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0).OrderByDescending(m => m.Id).FirstOrDefault().ResellerPrice : 0,
                                 CustomerPrice = (db.CustomerPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == Id))
                                                    ? (db.CustomerPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == Id)).OrderByDescending(m => m.Id).FirstOrDefault().Price
                                                    : (db.ResellerCustomersPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == Id && m.CustomerPrice != null)) ?
                                                    db.ResellerCustomersPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == Id).OrderByDescending(m => m.Id).FirstOrDefault().CustomerPrice
                                                    : op.Max(x => x.CustomerPrice),
                                 AgreementType = op.Max(x => x.AgreementType),
                                 CustomerType = op.Max(x => x.CustomerType),
                                 LicenseType = op.Max(x => x.LicenseType),
                                 PurchaseUnit = op.Max(x => x.PurchaseUnit),
                                 PurchaseUnitNumber = op.Max(x => x.PurchaseUnitNumber),
                                 Id = op.Max(x => x.Id),
                                 DefaultMarginReseller = db.Resellers.Where(m => m.Id == Id).FirstOrDefault().Margin   //get the margin from reseller table
                             }).ToList();



                List<Models.GridPriceList> mic = query.ToList().Select(r => new Models.GridPriceList
                {
                    EndDate = r.EndDate,
                    Id = r.Id,
                    MicrosoftId = r.MicrosoftId,
                    Name = r.Name,
                    StartDate = r.StartDate,
                    // Date = r.StartDate.Date + "<br/> "+r.EndDate.Date,
                    Price = r.Price,
                    AgreementType = AgreementTypes.ParseEnumToString<AgreementType>(r.AgreementType),
                    CustomerType = CustomerTypes.ParseEnumToString<CustomerType>(r.CustomerType),
                    LicenseType = LicenseTypes.ParseEnumToString<LicenseType>(r.LicenseType),
                    ResellerPrice = (double)r.ResellerPrice != 0 ? (double)r.ResellerPrice : (r.Price + ((r.CustomerPrice - r.Price) * r.DefaultMarginReseller) / 100),
                    ResellerPricePercentage = ((double)System.Math.Round(((double)((((double)r.ResellerPrice != 0 ?
                    (double)r.ResellerPrice
                    : (r.Price + ((r.CustomerPrice - r.Price) * r.DefaultMarginReseller) / 100)) - r.Price) * 100) / r.Price), 2)).ToString(),
                    //((double)System.Math.Round(((double)((((double)r.ResellerPrice != 0 ? (double)r.ResellerPrice : (r.CustomerPrice - (r.CustomerPrice * r.DefaultMarginReseller) / 100)) - r.Price) * 100) / r.Price), 2)).ToString(),
                    CustomerPrice = r.CustomerPrice,
                    CustomerPricePercentage = ((double)System.Math.Round((((r.CustomerPrice - ((double)r.ResellerPrice != 0 ? (double)r.ResellerPrice : (r.Price + ((r.CustomerPrice - r.Price) * r.DefaultMarginReseller) / 100))) * 100) / ((double)r.ResellerPrice != 0 ? (double)r.ResellerPrice : (r.Price + ((r.CustomerPrice - r.Price) * r.DefaultMarginReseller) / 100))), 2)).ToString(),
                    PurchaseUnit = r.PurchaseUnitNumber + " " + PurchaseUnits.ParseEnumToString<PurchaseUnit>(r.PurchaseUnit),
                    //PurchaseUnitNumber = r.PurchaseUnitNumber,
                    DefaultMarginReseller = (double)r.ResellerPrice != 0 ?

                   ((double)((

                   ((double)System.Math.Round((((r.CustomerPrice - r.Price) * 100) / r.Price), 2))
                   -
                    ((double)System.Math.Round(((double)((((double)r.ResellerPrice != 0 ?
                    (double)r.ResellerPrice
                    : (r.Price + ((r.CustomerPrice - r.Price) * r.DefaultMarginReseller) / 100)) - r.Price) * 100) / r.Price), 2)))
                    * 100
                    )
                / ((double)(((double)System.Math.Round((((r.CustomerPrice - r.Price) * 100) / r.Price), 2))))
                    ).ToString()
                    : r.DefaultMarginReseller.ToString()
                }).OrderBy(m => m.Name).ToList();

                return mic;
            }
            catch
            {
                return new List<Models.GridPriceList>();
            }
        }

        public List<Models.MicrosoftPriceList> GetPriceList(int ResellerId, string Id)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();

                var query = (from p in db.MicrosoftPriceList
                             where p.Status == true && p.MicrosoftId == Id
                             group p by p.MicrosoftId into op
                             select new
                             {
                                 MicrosoftId = op.Key,
                                 Price = op.Max(x => x.Price),
                                 ResellerPrice = db.ResellerPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == ResellerId && m.Status == true) ?
                                                 db.ResellerPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == ResellerId && m.Status == true).OrderByDescending(m => m.Id).FirstOrDefault().Price :
                                                (db.ResellerCustomersPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0 && m.ResellerPrice != null))
                                                 ? db.ResellerCustomersPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0).OrderByDescending(m => m.Id).FirstOrDefault().ResellerPrice : 0,
                                 CustomerPrice = (db.CustomerPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == ResellerId))
                                                    ? (db.CustomerPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == ResellerId)).OrderByDescending(m => m.Id).FirstOrDefault().Price
                                                    : (db.ResellerCustomersPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == ResellerId && m.CustomerPrice != null)) ?
                                                    db.ResellerCustomersPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == ResellerId).OrderByDescending(m => m.Id).FirstOrDefault().CustomerPrice
                                                    : op.Max(x => x.CustomerPrice),
                                 PurchaseUnit = op.Max(x => x.PurchaseUnit),
                                 PurchaseUnitNumber = op.Max(x => x.PurchaseUnitNumber),
                                 Id = op.Max(x => x.Id),
                                 DefaultMarginReseller = db.Resellers.Where(m => m.Id == ResellerId).FirstOrDefault().Margin   //get the margin from reseller table

                             }).ToList();

                List<Models.MicrosoftPriceList> mic = query.ToList().Select(r => new Models.MicrosoftPriceList
                {
                    Id = r.Id,
                    MicrosoftId = r.MicrosoftId,
                    Price = r.Price,
                    ResellerPrice = (double)r.ResellerPrice != 0 ? (double)r.ResellerPrice : (r.Price + ((r.CustomerPrice - r.Price) * r.DefaultMarginReseller) / 100),
                    CustomerPrice = r.CustomerPrice,
                    PurchaseUnit = r.PurchaseUnit,
                    PurchaseUnitNumber = r.PurchaseUnitNumber,

                }).ToList();
                return mic;
            }
            catch
            {
                return new List<Models.MicrosoftPriceList>();
            }
        }
    }
}