using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Areas.Reseller.Models
{
    public class ResellerLogin
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}