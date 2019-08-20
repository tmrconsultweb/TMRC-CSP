using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Areas.Reseller.Models
{
    [Table("tbl_ResellerCustomersPrice")]
    public class ResellerCustomersPrice
    {
        public int Id { get; set; }
        public int ResellerId { get; set; }
        public int PriceId { get; set; }
        public double CustomerPrice { get; set; }
        public double? ResellerPrice { get; set; }
    }
}