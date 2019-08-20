using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_SaleItems")]
    public class SaleItems
    {
        [Key]
        public int Id { get; set; }
        public string OfferId { get; set; }
        public double OriginalPrice { get; set; }
        public int License { get; set; }
        public double DiscountPrice { get; set; }
        public int SaleId { get; set; }
        public int PurchaseUnit { get; set; }
        public int PurchaseUnitNumber { get; set; }
        public string OrderId { get; set; }
    }
}