using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_ResellerPrice")]
    public class ResellerPrice
    {
        public int Id { get; set; }
        public int ResellerId { get; set; }   // ResellerId means admin   has change the price of which reseller (denoted by ChangeAgainstId)
        public int PriceId { get; set; }
        [Column("ResellerPrice")]
        public double Price { get; set; }
        public bool Status { get; set; }
    }
}