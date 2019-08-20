using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public class MicrosoftOfferAndPriceListRepo
    {
        public IEnumerable<Models.MicrosoftOffer> MicrosoftOffer { get; set; }
        public List<Models.MicrosoftPriceList> MicrosoftPriceList { get; set; }
    }
}