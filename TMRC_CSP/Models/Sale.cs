using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_sale")]
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public int ResellerId { get; set; }
        public string CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public double DiscountMargin { get; set; }
        public Int64 InvoiceNo { get; set; }
    }
}