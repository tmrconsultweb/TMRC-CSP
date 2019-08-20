using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_OffersSetup")]
    public class OffersSetup
    {
        [Key]
        public int Id { get; set; }
        public string MicrosoftOfferId { get; set; }
        public Int64 Price { get; set; }
        public bool Status { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime AppliedDate { get; set; }
        public string Summary { get; set; }
        public string Features { get; set; }
    }
}