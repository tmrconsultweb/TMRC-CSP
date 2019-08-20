using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public class CartMicrosoftPriceRepo
    {
        public int CartId { get; set; }
        public int License { get; set; }
        public string MicrosoftId { get; set; }
        public string Name { get; set; }
        public DateTime OrderTime { get; set; }

        public double Price { get; set; }   

        public double ERPrice { get; set; }
        public double TotalERPrice { get; set; }
        
        public double ResellerPrice { get; set; }

        public int PurchaseUnit { get; set; }
        public int PurchaseUnitNumber { get; set; }

    }
}