using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMRC_CSP.Models;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    public class OfferCatalogViewModel
    {
        /// <summary>
        /// Gets or sets the partner offers.
        /// </summary>
        public IEnumerable<PartnerOffer> Offers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the portal has been configured or not.
        /// </summary>
        public bool IsPortalConfigured { get; set; }
    }
}