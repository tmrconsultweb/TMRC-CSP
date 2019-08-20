using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Areas.Reseller.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage ="Old Password is Required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^.*(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*\(\)_\-+=]).*$", ErrorMessage = "Password Should be 1 Special Character, 1 Uppercase letter, 1 Digit")]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "Password length should be Minimum 9 Characters")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New Password is Required")]
        [DataType(DataType.Password)]   
        [RegularExpression(@"^.*(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*\(\)_\-+=]).*$", ErrorMessage = "Password Should be 1 Special Character, 1 Uppercase letter, 1 Digit")]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "Password length should be Minimum 9 Characters")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare("NewPassword",ErrorMessage ="Password does not match")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^.*(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*\(\)_\-+=]).*$", ErrorMessage = "Password Should be 1 Special Character, 1 Uppercase letter, 1 Digit")]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "Password length should be Minimum 9 Characters")]
        public string ConfirmPassword { get; set; }
    }
}