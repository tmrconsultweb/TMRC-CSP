using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMRC_CSP.ViewModel.PartnerRepository;

namespace TMRC_CSP.ViewModel.Invoice
{
    public class Invoice
    {

        //Get invoice for admin
        public Models.Invoice Get(string CustomerId,int AgentId=0)
        {
            try
            {
                Models.Invoice invoice = new Models.Invoice();

                //Generate Invoice Number
                Random random = new Random();
                invoice.InvoiceNumber = random.Next(0, 9999999).ToString("D6");
                HttpContext.Current.Session["InvoiceNo"] = invoice.InvoiceNumber;

                //Get Customer information from db
                ViewModel.PartnerRepository.Customers customers = new ViewModel.PartnerRepository.Customers(ApplicationDomain.Instance);
                invoice.Customers = customers.GetCustomerById(CustomerId);

                //If not exist in db then Api will use
                if(invoice.Customers == null)
                    invoice.Customers = customers.GetAPICustomerById(CustomerId);
                

                //get cart items
                ViewModel.AddToCart.AddToCart cart = new ViewModel.AddToCart.AddToCart();
                invoice.Cart = cart.Get(AgentId, CustomerId, true);

                //Get sale tax
                ViewModel.SaleTax.SaleTax saleTax = new ViewModel.SaleTax.SaleTax();
                invoice.SaleTax = saleTax.Get(invoice.Customers.Country, invoice.Customers.Province, AgentId);

                return invoice;

            }
            catch
            {
                return new Models.Invoice();
            }
        }


        
    }
}