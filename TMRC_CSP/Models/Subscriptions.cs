using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_Subscriptions")]
    public class Subscriptions
    {
        [Key]
        public int Id { get; set; }
        public string MicrosoftOfferId { get; set; }
        public int CustomerId { get; set; }
        public int Licenses { get; set; }
        public Int64 TotalPrice { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}