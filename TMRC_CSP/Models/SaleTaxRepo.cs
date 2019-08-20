using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public class SaleTaxRepo
    {
        public int SaleTaxId { get; set; }
        public double Amount { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string State { get; set; }
    }
}