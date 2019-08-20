using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TMRC_CSP.Models;

namespace TMRC_CSP.ViewModel.Context
{
    public class ConnectionStringsContext : DbContext
    {
        public DbSet<Models.Customers> Customers { get; set; }
        public DbSet<Models.OffersSetup> OffersSetup { get; set; }
        public DbSet<Models.Subscriptions> Subscriptions { get; set; }
        public DbSet<Models.Countries> Countries { get; set; }
        public DbSet<Models.MicrosoftPriceList> MicrosoftPriceList { get; set; }
        public DbSet<Models.TermsConditions> TermsConditions { get; set; }
        public DbSet<Models.DefaultMargin> DefaultMargin { get; set; }
        public DbSet<Models.PromotionCodes> PromotionCodes { get; set; }

        public System.Data.Entity.DbSet<TMRC_CSP.Models.Reseller> Resellers { get; set; }

        public DbSet<TMRC_CSP.Areas.Reseller.Models.ResellerCustomersPrice> ResellerCustomersPrice { get; set; }
        public DbSet<Models.Cart> Cart { get; set; }

        public DbSet<Models.Sale> Sale { get; set; }
        public DbSet<Models.SaleItems> SaleItems { get; set; }

        public DbSet<Models.ResellerPrice> ResellerPrice { get; set; }

        public DbSet<Models.CustomerPrice> CustomerPrice { get; set; }

        public DbSet<Models.SaleTax> SaleTax { get; set; }

        public DbSet<Models.Company> Company { get; set; }
        public DbSet<Models.InvoiceSetting> InvoiceSetting { get; set; }
    }
}