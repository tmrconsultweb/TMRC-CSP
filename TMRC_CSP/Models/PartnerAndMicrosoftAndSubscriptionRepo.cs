using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public class PartnerAndMicrosoftAndSubscriptionRepo
    {
        public List<PartnerAndMicrosoftRepository> PartnerAndMicrosoftRepository { get; set; }
        public List<Subscriptions> Subscriptions { get; set; }
        public List<Models.MicrosoftPriceList> MicrosoftPriceList { get; set; }
    }
}