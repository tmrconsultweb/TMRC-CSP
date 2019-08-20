using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("Country")]
    public class Countries
    {
        [Key]
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
    }

    public class ddlCountry
    {
        //CountryName
        public string Text { get; set; }
        //Code
        public string Value { get; set; }
    }
}