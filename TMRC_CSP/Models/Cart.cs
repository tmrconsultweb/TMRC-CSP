using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_Cart")]
    public class Cart
    {
        public int Id { get; set; }
        public string MicrosoftId { get; set; }
        public int License { get; set; }
        public DateTime OrderTime { get; set; }
        public int AgentId { get; set; }
        public bool Status { get; set; }
        public int PurchaseUnit { get; set; }
        public int PurchaseUnitNumber { get; set; }
        public string CustomerId { get; set; }
    }
}