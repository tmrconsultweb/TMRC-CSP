using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_Reseller")]
    public class Reseller
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="First Name is required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string LastName { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^((\+92)|(0092))-{0,1}\d{3}-{0,1}\d{7}$|^\d{11}$|^\d{4}-\d{7}$",ErrorMessage ="Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage ="Invalid Email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Zip Code is required")]
        public string ZipCode { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        //[Required(ErrorMessage = "Password is Required")]
        //[DataType(DataType.Password)]
        //[RegularExpression(@"^.*(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*\(\)_\-+=]).*$", ErrorMessage = "Password Should be 1 Special Character, 1 Uppercase letter, 1 Digit")]
        //[StringLength(20, MinimumLength = 9, ErrorMessage = "Password length should be Minimum 9 Characters")]
        public string Password { get; set; }
        public bool Is1stTimePassChg { get; set; }

        [Required(ErrorMessage = "Reseller Margin percentage is required")]
        public double Margin { get; set; }
    }
}