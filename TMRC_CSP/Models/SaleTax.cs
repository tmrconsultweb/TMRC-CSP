using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_SaleTax")]
    public class SaleTax
    {
        [Key]
        public int Id { get; set; }
        public int ResellerId { get; set; }
        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        [Required(ErrorMessage ="Sale tax is required and should be in percentage")]
        [Display(Name ="Sale Tax")]
        public double Amount { get; set; }
        public bool Status { get; set; }
    }
}