using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public class SubscriptionsSummary
    {
        /// <summary>
        /// Gets or sets the subscription's in this summary.
        /// </summary>
        public IEnumerable<SubscriptionViewModel> Subscriptions { get; set; }

        /// <summary>
        /// Gets or sets the total amount for this subscription summary.
        /// </summary>
        public string SummaryTotal { get; set; }

        /// <summary>
        /// Gets or sets Customer view model
        /// </summary>
        public OffersSetup CustomerViewModel { get; set; }
    }
}