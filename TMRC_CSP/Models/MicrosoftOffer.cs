using Microsoft.Store.PartnerCenter.Models.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    /// <summary>
    /// Represents a Microsoft offer.
    /// </summary>
    public class MicrosoftOffer
    {
        /// <summary>
        /// Bit to check he/she want thats offer or not
        /// </summary>
        public bool IsAccept { get; set; }
        
        /// <summary>
        /// The selected id of the offers
        /// </summary>
        public string Offerid { get; set; }

        /// <summary>
        /// The Price of the offers
        /// </summary>
        public string Price { get; set; }


        /// <summary>
        /// Gets or sets the Microsoft offer details.
        /// </summary>
        public Offer Offer { get; set; }

        /// <summary>
        /// Gets or sets the offer's thumbnail URI.
        /// </summary>
        public Uri ThumbnailUri { get; set; }
    }
}