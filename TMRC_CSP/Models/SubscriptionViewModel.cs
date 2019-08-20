using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public class SubscriptionViewModel
    {
        /// <summary>
        /// Gets or sets the subscription's Id.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the subscription's portal offer Id.
        /// </summary>
        public string PortalOfferId { get; set; }

        /// <summary>
        /// Gets or sets the subscription's portal offer Price.
        /// </summary>
        public string PortalOfferPrice { get; set; }

        /// <summary>
        /// Gets or sets the subscription's friendly name.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether subscription is renewable. 
        /// </summary>
        public bool IsRenewable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether subscription is editable. 
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        /// Gets or sets the total number of licenses for this subscription.
        /// </summary>
        public string LicensesTotal { get; set; }

        /// <summary>
        /// Gets or sets the total amount for this subscription.
        /// </summary>
        public string SubscriptionTotal { get; set; }

        /// <summary>
        /// Gets or sets the annual commitment expiration date for this subscription.
        /// </summary>
        public string SubscriptionExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the remaining days to expiry for this subscription.
        /// </summary>
        public decimal SubscriptionProRatedPrice { get; set; }

        /// <summary>
        /// Gets or sets the subscription's order history.
        /// </summary>
        public IEnumerable<SubscriptionHistory> SubscriptionOrderHistory { get; set; }
    }
}