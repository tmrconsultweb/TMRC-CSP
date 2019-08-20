using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_CustomerPrice")]
    public class CustomerPrice
    {
        [Key]
        public int Id { get; set; }
        public int ResellerId { get; set; }   // ResellerId means which reseller has change the Customer Price (denoted by ChangeById)
        public int PriceId { get; set; }
        [Column("CustomerPrice")]
        public double Price { get; set; }
        public bool Status { get; set; }
    }
}