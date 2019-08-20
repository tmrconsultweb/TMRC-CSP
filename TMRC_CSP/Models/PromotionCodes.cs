using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_PromotionCodes")]
    public class PromotionCodes
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Expiry date is required")]
        public DateTime ExpiryDate { get; set; }
        public bool IsApplied { get; set; }
        public bool Status { get; set; }

        [Required(ErrorMessage ="Amount is required")]
        public double Amount { get; set; }
    }
}