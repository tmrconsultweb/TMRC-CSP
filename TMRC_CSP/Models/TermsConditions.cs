using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_Terms&Conditions")]
    public class TermsConditions
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Terms & Conditions Message")]
        [Required(ErrorMessage ="Terms and Conditions is required")]
        public string Info { get; set; }
        public DateTime CreatedDate{ get; set; }
        public int Role { get; set; }
        public int ResellerId { get; set; }
    }
}