using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMRC_CSP.Models;

namespace TMRC_CSP.ViewModel.MicrosoftPriceList
{
    public class PriceList
    {
        public void SavePriceList(List<Models.ExcelPriceList> _m)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                foreach (var lm in _m)
                {
                    if (((Models.OfferActionType)lm.ActionType == Models.OfferActionType.ADD || (Models.OfferActionType)lm.ActionType == Models.OfferActionType.CHG) && !db.MicrosoftPriceList.Any(m => m.MicrosoftId == lm.MicrosoftId && m.Price == lm.Price && m.CustomerPrice == lm.CustomerPrice && m.AgreementType == lm.AgreementType && m.CustomerType == lm.CustomerType && m.LicenseType == lm.LicenseType))
                    {
                        // A new offer added 
                        //if (!db.MicrosoftPriceList.Any(m => m.MicrosoftId == lm.MicrosoftId))
                        //{
                        Models.MicrosoftPriceList microsoftPriceList = SaveList(lm, true);
                        db.MicrosoftPriceList.Add(microsoftPriceList);
                        //}

                    }
                    else if ((Models.OfferActionType)lm.ActionType == Models.OfferActionType.DEL)    // soft Delete the rec
                    {
                        var p = db.MicrosoftPriceList.Where(m => m.MicrosoftId == lm.MicrosoftId).SingleOrDefault();
                        if (p == null) //if not exit then add and aplly soft deletion action
                        {
                            Models.MicrosoftPriceList microsoftPriceList = SaveList(lm, false);
                            db.MicrosoftPriceList.Add(microsoftPriceList);
                        }
                        else if (p.Status == true) // if exist and it's status is true then soft deletion action occur
                        {
                            p.Status = false;
                        }
                    }
                    else if ((Models.OfferActionType)lm.ActionType == Models.OfferActionType.UNC)  // do not change record
                    {
                        var p = db.MicrosoftPriceList.Where(m => m.MicrosoftId == lm.MicrosoftId).SingleOrDefault();
                        if (p == null) // if not exit then add a new data otherwise do not apllying any changes if it's exist
                        {
                            Models.MicrosoftPriceList microsoftPriceList = SaveList(lm, true);
                            db.MicrosoftPriceList.Add(microsoftPriceList);
                        }

                    }

                    //if (!db.MicrosoftPriceList.Any(m => m.MicrosoftId == lm.MicrosoftId && m.EndDate == lm.EndDate && m.StartDate == lm.StartDate && m.Price == lm.Price))
                    //{
                    //    Models.MicrosoftPriceList microsoftPriceList = new Models.MicrosoftPriceList
                    //    {
                    //        CustomerPrice = lm.CustomerPrice,
                    //        EndDate = lm.EndDate,
                    //        MicrosoftId = lm.MicrosoftId,
                    //        Price = lm.Price,
                    //        PurchaseUnit = lm.PurchaseUnit,
                    //        PurchaseUnitNumber = lm.PurchaseUnitNumber,
                    //        ResellerPrice = lm.ResellerPrice,
                    //        StartDate = lm.StartDate,
                    //    };
                    //    db.MicrosoftPriceList.Add(microsoftPriceList);
                    //}

                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        private Models.MicrosoftPriceList SaveList(ExcelPriceList lm, bool status)
        {
            Models.MicrosoftPriceList microsoftPriceList = new Models.MicrosoftPriceList
            {
                CustomerPrice = lm.CustomerPrice,
                EndDate = lm.EndDate,
                MicrosoftId = lm.MicrosoftId,
                Name = lm.Name,
                Price = lm.Price,
                PurchaseUnit = lm.PurchaseUnit,
                PurchaseUnitNumber = lm.PurchaseUnitNumber,
                ResellerPrice = lm.ResellerPrice,
                StartDate = lm.StartDate,
                Status = status,
                AgreementType = lm.AgreementType,
                CustomerType = lm.CustomerType,
                LicenseType = lm.LicenseType
            };
            return microsoftPriceList;
        }


        //Find the buying price, ERP Price, Reseller Price
        public List<Models.MicrosoftPriceList> Get()
        {
            try
            {
                var db = new Context.ConnectionStringsContext();

                var query = (from p in db.MicrosoftPriceList
                             where p.Status == true
                             group p by p.MicrosoftId into op
                             select new
                             {
                                 MicrosoftId = op.Key,
                                 Price = op.Max(x => x.Price),
                                 ResellerPrice = (db.ResellerCustomersPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0 && m.ResellerPrice != null)) ?
                                                db.ResellerCustomersPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0).FirstOrDefault().ResellerPrice : 0,
                                 CustomerPrice = (db.ResellerCustomersPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0 && m.CustomerPrice != null)) ?
                                                db.ResellerCustomersPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0).FirstOrDefault().CustomerPrice
                                                : op.Max(x => x.CustomerPrice),
                                 PurchaseUnit = op.Max(x => x.PurchaseUnit),
                                 PurchaseUnitNumber = op.Max(x => x.PurchaseUnitNumber),
                                 Id = op.Max(x => x.Id),
                                 DefaultMarginReseller = db.DefaultMargin.Where(m => m.ResellerId == 0 && m.Role == (int)Roles.Resellers).FirstOrDefault().DefaultPercentage

                                 //Price = op.Max(x => x.Price),
                                 //ResellerPrice = op.Max(x => x.ResellerPrice) == 0 ? op.Max(x => x.CustomerPrice) - (op.Max(x => x.CustomerPrice) * (db.DefaultMargin.Where(m => m.ResellerId == 0 && m.Role == (int)Roles.Resellers).FirstOrDefault().DefaultPercentage)) / 100 : op.Max(x => x.ResellerPrice),
                                 //CustomerPrice = op.Max(x => x.CustomerPrice),
                                 //PurchaseUnit = op.Max(x => x.PurchaseUnit),
                                 //PurchaseUnitNumber = op.Max(x => x.PurchaseUnitNumber),
                                 //Id = op.Max(x => x.Id),

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


        //List<Models.MicrosoftPriceList>
        public List<Models.ExcelPriceList> GetPriceList()
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                //var obj = (from n in db.MicrosoftPriceList
                //       group n by n.MicrosoftId into g
                //       select new { MicrosoftId = g.Key, StartDate = g.Max(t => t.StartDate) }).ToList() ;
                //return obj;
                //return db.MicrosoftPriceList.OrderByDescending(T => T.StartDate).Take(1).ToList();
                var query = (from p in db.MicrosoftPriceList
                             where p.Status == true
                             group p by p.MicrosoftId into op
                             select new
                             {
                                 MicrosoftId = op.Key,
                                 Name = op.Max(x => x.Name),
                                 StartDate = op.Max(x => x.StartDate),
                                 EndDate = op.Max(x => x.EndDate),
                                 Price = op.Max(x => x.Price),
                                 //ResellerPrice = op.Max(x => x.ResellerPrice),
                                 ResellerPrice = op.Max(x => x.ResellerPrice) == 0 ? op.Max(x => x.CustomerPrice) - (op.Max(x => x.CustomerPrice) * (db.DefaultMargin.Where(m => m.ResellerId == 0 && m.Role == (int)Roles.Resellers).FirstOrDefault().DefaultPercentage)) / 100 : op.Max(x => x.ResellerPrice),
                                 CustomerPrice = op.Max(x => x.CustomerPrice),
                                 AgreementType = op.Max(x => x.AgreementType),
                                 CustomerType = op.Max(x => x.CustomerType),
                                 LicenseType = op.Max(x => x.LicenseType),
                                 PurchaseUnit = op.Max(x => x.PurchaseUnit),
                                 PurchaseUnitNumber = op.Max(x => x.PurchaseUnitNumber),
                                 Id = op.Max(x => x.Id),

                             }).ToList();

                List<Models.ExcelPriceList> mic = query.ToList().Select(r => new Models.ExcelPriceList
                {
                    EndDate = r.EndDate,
                    Id = r.Id,
                    MicrosoftId = r.MicrosoftId,
                    Name = r.Name,
                    StartDate = r.StartDate,
                    Price = r.Price,
                    AgreementType = r.AgreementType,
                    CustomerType = r.CustomerType,
                    LicenseType = r.LicenseType,
                    ResellerPrice = r.ResellerPrice,
                    CustomerPrice = r.CustomerPrice,
                    PurchaseUnit = r.PurchaseUnit,
                    PurchaseUnitNumber = r.PurchaseUnitNumber,

                }).ToList();

                return mic;
            }
            catch (Exception ex)
            {
                return new List<Models.ExcelPriceList>();
            }
        }




        //For Admin (Overall price list => ResellerPrice > MicrosoftPriceList)
        public List<Models.GridPriceList> GetGridPriceListForAdmin()
        {
            //IEnumerable<MicrosoftOffer> microsoftOffers = await ApplicationDomain.Instance.OffersRepository.RetrieveMicrosoftOffersAsync().ConfigureAwait(false);
            try
            {
                var db = new Context.ConnectionStringsContext();
                var query = (from p in db.MicrosoftPriceList
                                 // join 
                             where p.Status == true
                             group p by p.MicrosoftId into op
                             select new
                             {
                                 MicrosoftId = op.Key,
                                 Name = op.Max(x => x.Name),
                                 StartDate = op.Max(x => x.StartDate),
                                 EndDate = op.Max(x => x.EndDate),
                                 Price = op.Max(x => x.Price),
                                 ResellerPrice = (db.ResellerCustomersPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0 && m.ResellerPrice != null))
                                                 ? db.ResellerCustomersPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0).OrderByDescending(m => m.Id).FirstOrDefault().ResellerPrice 
                                                 : 0,
                                 //op.Max(x => x.CustomerPrice) - (op.Max(x => x.CustomerPrice) * (db.DefaultMargin.Where(m => m.ResellerId == 0 && m.Role == (int)Roles.Resellers).FirstOrDefault().DefaultPercentage)) / 100,

                                 //ResellerPrice = (db.ResellerCustomersPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0 && m.ResellerPrice != null)) ?
                                 //               db.ResellerCustomersPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0).FirstOrDefault().ResellerPrice
                                 //               : op.Max(x => x.ResellerPrice),
                                 CustomerPrice = (db.ResellerCustomersPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0 && m.CustomerPrice != null)) ?
                                                db.ResellerCustomersPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0).OrderByDescending(m => m.Id).FirstOrDefault().CustomerPrice
                                                : op.Max(x => x.CustomerPrice),
                                 AgreementType = op.Max(x => x.AgreementType),
                                 CustomerType = op.Max(x => x.CustomerType),
                                 LicenseType = op.Max(x => x.LicenseType),
                                 PurchaseUnit = op.Max(x => x.PurchaseUnit),
                                 PurchaseUnitNumber = op.Max(x => x.PurchaseUnitNumber),
                                 Id = op.Max(x => x.Id),
                                 DefaultMarginReseller = db.DefaultMargin.Where(m => m.ResellerId == 0 && m.Role == (int)Roles.Resellers).FirstOrDefault().DefaultPercentage
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
                    ResellerPricePercentage = ((double)System.Math.Round(((double)(((
                    (double)r.ResellerPrice != 0
                    ? (double)r.ResellerPrice
                    : (r.Price + ((r.CustomerPrice - r.Price) * r.DefaultMarginReseller) / 100)
                    ) - r.Price) * 100) / r.Price), 2)).ToString(),
                    //ResellerPrice = (double)r.ResellerPrice != 0 ? (double)r.ResellerPrice : (r.CustomerPrice - (r.CustomerPrice * r.DefaultMarginReseller) / 100),
                    //ResellerPricePercentage = ((double)System.Math.Round(((double)((((double)r.ResellerPrice != 0 ? (double)r.ResellerPrice : (r.CustomerPrice - (r.CustomerPrice * r.DefaultMarginReseller) / 100)) - r.Price) * 100) / r.Price), 2)).ToString(),
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
            catch (Exception ex)
            {
                return new List<Models.GridPriceList>();
            }
        }

        //Get Price List by Id
        public Models.MicrosoftPriceList GetPriceById(string id)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();

                var query = (from p in db.MicrosoftPriceList
                             where p.Status == true && p.MicrosoftId == id
                             group p by p.MicrosoftId into op

                             select new
                             {
                                 MicrosoftId = op.Key,

                                 Price = op.Max(x => x.Price),
                                 //ResellerPrice = op.Max(x => x.ResellerPrice),
                                 //ResellerPrice = op.Max(x => x.ResellerPrice) == 0 ? op.Max(x => x.CustomerPrice) - (op.Max(x => x.CustomerPrice) * (db.DefaultMargin.Where(m => m.ResellerId == 0 && m.Role == (int)Roles.Resellers).FirstOrDefault().DefaultPercentage)) / 100 : op.Max(x => x.ResellerPrice),
                                 ResellerPrice = (db.ResellerCustomersPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0 && m.ResellerPrice != null)) ?
                                 db.ResellerCustomersPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0).FirstOrDefault().ResellerPrice : 0,
                                 CustomerPrice = (db.ResellerCustomersPrice.Any(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0 && m.CustomerPrice != null)) ?
                                                db.ResellerCustomersPrice.Where(m => m.PriceId == op.Max(x => x.Id) && m.ResellerId == 0).FirstOrDefault().CustomerPrice
                                                : op.Max(x => x.CustomerPrice), //op.Max(x => x.CustomerPrice),
                                 PurchaseUnit = op.Max(x => x.PurchaseUnit),
                                 PurchaseUnitNumber = op.Max(x => x.PurchaseUnitNumber),
                                 Id = op.Max(x => x.Id),
                                 DefaultMarginReseller = db.DefaultMargin.Where(m => m.ResellerId == 0 && m.Role == (int)Roles.Resellers).FirstOrDefault().DefaultPercentage
                             }).SingleOrDefault();

                Models.MicrosoftPriceList mic = new Models.MicrosoftPriceList
                {
                    Id = query.Id,
                    MicrosoftId = query.MicrosoftId,
                    Price = query.Price,
                    ResellerPrice = (double)query.ResellerPrice != 0 ? (double)query.ResellerPrice : (query.CustomerPrice - (query.CustomerPrice * query.DefaultMarginReseller) / 100),
                    CustomerPrice = query.CustomerPrice,
                    PurchaseUnit = query.PurchaseUnit,
                    PurchaseUnitNumber = query.PurchaseUnitNumber,

                };
                return mic;
            }
            catch
            {
                return new Models.MicrosoftPriceList();
            }
        }
    }
}