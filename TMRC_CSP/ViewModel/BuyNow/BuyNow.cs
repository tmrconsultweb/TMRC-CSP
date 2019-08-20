using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMRC_CSP.ViewModel.PartnerRepository;

namespace TMRC_CSP.ViewModel.BuyNow
{
    public class BuyNow
    {
        public string Buy(int ResellerId, string CustomerId, Int64 InvoiceNo, double DiscountMargin)
        {
            string OffersName = string.Empty;
            using (var db = new ViewModel.Context.ConnectionStringsContext())
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {

                try
                {

                    ViewModel.AddToCart.AddToCart cart = new ViewModel.AddToCart.AddToCart();
                    List<Models.CartMicrosoftPriceRepo> CartMicrosoftPriceRepo = cart.Get(0, CustomerId, true);

                    //Save the information on sale table
                    Models.Sale sale = new Models.Sale
                    {
                        CustomerId = CustomerId,
                        InvoiceNo = InvoiceNo,
                        ResellerId = ResellerId,
                        SaleDate = DateTime.Now,
                        DiscountMargin = DiscountMargin
                    };

                    db.Sale.Add(sale);
                    db.SaveChanges();

                    //Save the items in db
                    foreach (var item in CartMicrosoftPriceRepo)
                    {
                        ViewModel.Subscription.Subscriptions subscriptions = new Subscription.Subscriptions(ApplicationDomain.Instance);

                        OffersName += item.Name + ", ";
                        Microsoft.Store.PartnerCenter.Models.Orders.Order order = subscriptions.PlaceOrder(CustomerId, item.MicrosoftId, item.License, Models.PurchaseUnit.Months, item.Name);

                        Models.SaleItems saleItems = new Models.SaleItems
                        {
                            License = item.License,
                            OfferId = item.MicrosoftId,
                            OriginalPrice = (double)System.Math.Round(item.ERPrice, 2),
                            SaleId = sale.Id,
                            DiscountPrice = (double)System.Math.Round(item.ERPrice - (item.ERPrice * DiscountMargin) / 100, 2),
                            PurchaseUnitNumber = 0,
                            PurchaseUnit = 0,
                            OrderId = order.Id,

                        };
                        db.SaleItems.Add(saleItems);
                        db.SaveChanges();
                    }

                    // If items are sale then remove that items from cart
                    db.Cart.Where(m => m.AgentId == ResellerId && m.Status == true).ToList().ForEach(c => c.Status = false);
                    db.SaveChanges();

                    dbContextTransaction.Commit();

                    //Send email to customer
                    SendEmailOffer(OffersName, CustomerId);
                    

                    return "Successfully Items has been purchased";

                }
                catch
                {
                    dbContextTransaction.Rollback();
                    return "Unknown error occur, Please try again.";
                }
            }
        }

        private void SendEmailOffer(string OffersName,string customerId)
        {
            try
            {
                //string email = string.Empty;
                Customers viewCustomers = new Customers(ApplicationDomain.Instance);

                Models.Customers customers = viewCustomers.GetAPICustomerById(customerId);

                string MailBody = "<html><body>"
                   + "<p>Hi,</p>"
                   + customers.FirstName + ", "+ customers.LastName
                   + "<p>Now you can subscribe/login with the following offers</p>"
                   + OffersName.Substring(OffersName.Length - 2)
                   + "<br><br><p>Have a nice day!</p>"
                   + "</body></html>";
                ViewModel.SendEmail.SendEmailRegister(customers.Email, "Successfully Account has been created", MailBody);
            }
            catch
            {

            }
            
        }
        //public string Buy(int ResellerId, string CustomerId, Int64 InvoiceNo, double DiscountMargin)
        //{
        //    try
        //    {
        //        ViewModel.AddToCart.AddToCart cart = new ViewModel.AddToCart.AddToCart();
        //        List<Models.CartMicrosoftPriceRepo> CartMicrosoftPriceRepo = cart.Get(0, CustomerId, true);
        //        var db = new ViewModel.Context.ConnectionStringsContext();

        //        //Save the information on sale table
        //        Models.Sale sale = new Models.Sale
        //        {
        //            CustomerId=CustomerId,
        //            InvoiceNo=InvoiceNo,
        //            ResellerId=ResellerId,
        //            SaleDate= DateTime.Now,
        //            DiscountMargin = DiscountMargin
        //        };

        //        db.Sale.Add(sale);
        //        db.SaveChanges();

        //        //Save the items in db
        //        foreach(var item in CartMicrosoftPriceRepo)
        //        {
        //            ViewModel.Subscription.Subscriptions subscriptions = new Subscription.Subscriptions(ApplicationDomain.Instance);


        //            Microsoft.Store.PartnerCenter.Models.Orders.Order order = subscriptions.PlaceOrder(CustomerId, item.MicrosoftId, item.License, Models.PurchaseUnit.Months, item.Name);

        //            Models.SaleItems saleItems = new Models.SaleItems
        //            {
        //                License = item.License,
        //                OfferId = item.MicrosoftId,
        //                OriginalPrice = (double)System.Math.Round(item.ERPrice,2),
        //                SaleId = sale.Id,
        //                DiscountPrice = (double)System.Math.Round(item.ERPrice - (item.ERPrice * DiscountMargin) / 100, 2),
        //                PurchaseUnitNumber = 0,
        //                PurchaseUnit = 0,
        //                OrderId = order.Id,

        //            };
        //            db.SaleItems.Add(saleItems);
        //            db.SaveChanges();
        //        }

        //        // If items are sale then remove that items from cart
        //        db.Cart.Where(m=>m.AgentId == ResellerId && m.Status == true).ToList().ForEach(c => c.Status = false);
        //        db.SaveChanges();

        //        return "Successfully Items has been purchased";
        //    }
        //    catch
        //    {
        //        return "Unknown error occur, Please try again.";
        //    }
        //}
    }
}