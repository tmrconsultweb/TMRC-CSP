using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_CustomerRegistration")]
    public class Customers
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="First Name is required")]  
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^((\+92)|(0092))-{0,1}\d{3}-{0,1}\d{7}$|^\d{11}$|^\d{4}-\d{7}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }
        [Display(Name = "Company")]
        [Required(ErrorMessage = "Company name is required")]
        public string Company { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Display(Name = "Address1")]
        [Required(ErrorMessage = "Address1 is required")]
        public string Address1 { get; set; }
        [Display(Name = "Address2")]
        //[Required(ErrorMessage = "Address2 is required")]
        public string Address2 { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Display(Name = "Province")]
        [Required(ErrorMessage = "Province is required")]
        public string Province { get; set; }
        [Display(Name = "ZipCode")]
        [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; }
        [Display(Name = "Additional Information")]
        public string AdditionalInfo { get; set; }

        public bool IsVerified { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime VerifiedDate { get; set; }
        [Display(Name = "Terms & Conditions")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The field must be checked.")]
        public bool IsAcceptTerms { get; set; }
        [Display(Name = "Primary Domain")]
        [Required(ErrorMessage = "Primary Domain is required")]
        public string PrimaryDomain { get; set; }
        public string MicrosoftId { get; set; }
        public int ResellerId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        
    }
}