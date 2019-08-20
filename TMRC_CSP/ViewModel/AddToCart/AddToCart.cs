using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TMRC_CSP.ViewModel.PartnerRepository;

namespace TMRC_CSP.ViewModel.AddToCart
{
    public class AddToCart
    {

        public string Save(string Id, int License, int AgentId, string CustomerId)
        {
            try
            {
                var db = new ViewModel.Context.ConnectionStringsContext();

                Models.Cart cart = new Models.Cart
                {
                    AgentId = AgentId,
                    License = License,
                    MicrosoftId = Id,
                    OrderTime = DateTime.Now,
                    Status = true,
                    PurchaseUnit = 2,
                    PurchaseUnitNumber = 12,
                    CustomerId = CustomerId,
                };

                db.Cart.Add(cart);
                db.SaveChanges();
                return "Successfully Add";
            }
            catch
            {
                return "Unknown error occur, Please try again.";
            }
        }

        public List<Models.CartMicrosoftPriceRepo> Get(int AgentId, string CustomerId, bool status = true)
        {
            try
            {
                var db = new ViewModel.Context.ConnectionStringsContext();

                //if (AgentId == 0)  //Get the admin price list
                //{
                    var query = (from c in db.Cart
                                 join p in db.MicrosoftPriceList
                                 on c.MicrosoftId equals p.MicrosoftId
                                 where c.AgentId == AgentId && c.Status == status && c.CustomerId == CustomerId
                                 select new
                                 {
                                     c.Id,
                                     c.License,
                                     c.MicrosoftId,
                                     c.OrderTime,
                                     c.PurchaseUnit,
                                     c.PurchaseUnitNumber,
                                     p.ResellerPrice,
                                    CustomerPrice = AgentId != 0   //For reseller price list
                                                    ? (db.CustomerPrice.Any(m => m.PriceId == p.Id && m.ResellerId == AgentId))
                                                    ? (db.CustomerPrice.Where(m => m.PriceId == p.Id && m.ResellerId == AgentId)).OrderByDescending(m => m.Id).FirstOrDefault().Price
                                                    : (db.ResellerCustomersPrice.Any(m => m.PriceId == p.Id && m.ResellerId == AgentId && m.CustomerPrice != null)) ?
                                                    db.ResellerCustomersPrice.Where(m => m.PriceId == p.Id && m.ResellerId == AgentId).OrderByDescending(m => m.Id).FirstOrDefault().CustomerPrice
                                                    : p.CustomerPrice
                                                    : (db.ResellerCustomersPrice.Any(m => m.PriceId == p.Id && m.ResellerId == 0 && m.CustomerPrice != null)) ?
                                                    db.ResellerCustomersPrice.Where(m => m.PriceId == p.Id && m.ResellerId == 0).OrderByDescending(m => m.Id).FirstOrDefault().CustomerPrice
                                                    : p.CustomerPrice,
                                     Total = p.CustomerPrice * c.License,
                                     Name = p.Name,
                                 }).ToList();
                //}



                List<Models.CartMicrosoftPriceRepo> list = query.ToList().Select(r => new Models.CartMicrosoftPriceRepo
                {
                    License = r.License,
                    OrderTime = r.OrderTime,
                    ResellerPrice = r.ResellerPrice,
                    MicrosoftId = r.MicrosoftId,
                    PurchaseUnit = r.PurchaseUnit,
                    PurchaseUnitNumber = r.PurchaseUnitNumber,
                    Name = r.Name,
                    ERPrice = (double)r.CustomerPrice,
                    TotalERPrice = r.Total,
                    Price = 0,
                    CartId = r.Id,
                }).ToList();
                return list;
            }
            catch
            {
                return new List<Models.CartMicrosoftPriceRepo>();
            }
        }


        //hard deletion of item from cart page
        public string Delete(int id, int AgentId)
        {
            try
            {
                var db = new ViewModel.Context.ConnectionStringsContext();
                var item = db.Cart.Where(m => m.Id == id && m.AgentId == AgentId).SingleOrDefault();
                db.Cart.Remove(item);
                db.SaveChanges();
                return "Deleted";
            }
            catch
            {
                return "Unknown error occur, Please try again.";
            }
        }


        //hard delete all of items at once from cart page
        public void DeleteAll(int AgentId)
        {
            try
            {
                var db = new ViewModel.Context.ConnectionStringsContext();
                var item = db.Cart.Where(m => m.AgentId == AgentId).ToList();
                db.Cart.RemoveRange(item);
                db.SaveChanges();
            }
            catch
            {

            }
        }

        public string UpdateById(int id, int license, string billingFrequency)
        {
            try
            {
                var db = new ViewModel.Context.ConnectionStringsContext();

                var cart = db.Cart.Where(m => m.Id == id && m.Status == true).SingleOrDefault();
                cart.License = license;
                cart.PurchaseUnit = 2;
                cart.PurchaseUnitNumber = Convert.ToInt16(billingFrequency.Replace("M", ""));

                db.SaveChanges();
                return "Successfully Updated.";
            }
            catch
            {
                return "Unknown error occur, Please try again.";
            }
        }
    }
}