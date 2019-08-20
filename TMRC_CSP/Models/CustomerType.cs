using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public enum CustomerType
    {
        [Display(Name = "CLOUD RESELLER CORPORATE")]
        CLOUD_RESELLER_CORPORATE = 1,
        [Display(Name = "CLOUD RESELLER CHARITY")]
        CLOUD_RESELLER_CHARITY = 2,
        [Display(Name = "CLOUD RESELLER FACULTY")]
        CLOUD_RESELLER_FACULTY = 3,
        [Display(Name = "CLOUD RESELLER STUDENT")]
        CLOUD_RESELLER_STUDENT = 4,
        [Display(Name = "CLOUD RESELLER GOVERNMENT")]
        CLOUD_RESELLER_GOVERNMENT = 5
    }
    public static class CustomerTypes
    {
        public static CustomerType ParseEnum<CustomerType>(string value)
        {
            value = value.Replace(' ','_').ToString();
            return (CustomerType)Enum.Parse(typeof(CustomerType),value , true);
        }

        public static string ParseEnumToString<CustomerType>(int value)
        {
            string customerType = Enum.GetName(typeof(TMRC_CSP.Models.CustomerType), value);
            if (customerType.Contains("_"))
                customerType = customerType.Replace("_", " ");
            return customerType;
        }
    }
}