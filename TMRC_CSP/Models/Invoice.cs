using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public class Invoice
    {
        public List<Models.CartMicrosoftPriceRepo> Cart { get; set; }
        public Models.Customers Customers { get; set; }
        public Models.SaleTax SaleTax { get; set; }
        public string InvoiceNumber { get; set; }
    }
}