using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public class CustomerAccount
    {
        public Models.Customers RequestInfo { get; set; }
        public bool IsAcceptTerms { get; set; }
        public string PrimaryDomain { get; set; }
        public DateTime CreatedDate { get; set; }
        public string MicrosoftId { get; set; }
    }
}