using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_Company")]
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public int ResellerId { get; set; }
        [Display(Name="Company Logo")]
        public string LogoUrl { get; set; }
        [Required(ErrorMessage ="Company Name is required")]
        [Display(Name = "Company Name")]
        public string Name { get; set; }
        [Display(Name = "Website Url")]
        public string WebsiteUrl { get; set; }
        [Required(ErrorMessage = "Company Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Company Phone Number is required")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "P.O.Box#")]
        public string POBox { get; set; }
        public string Address { get; set; }
    }
}