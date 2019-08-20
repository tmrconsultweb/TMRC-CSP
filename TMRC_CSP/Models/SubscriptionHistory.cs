using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public class SubscriptionHistory
    {
        /// <summary>
        /// Gets or sets number of seats bought for the subscription.
        /// </summary>
        public string SeatsBought { get; set; }

        /// <summary>
        /// Gets or sets the price at which the subscription was ordered.
        /// </summary>
        public string PricePerSeat { get; set; }

        /// <summary>
        /// Gets or sets the price at which the subscription was ordered.
        /// </summary>
        public string OrderTotal { get; set; }

        /// <summary>
        /// Gets or sets the transaction date.
        /// </summary>
        public string OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the operation type (NewPurchase, AdditionalSeatsPurchase, Renewal) 
        /// </summary>
        public string OperationType { get; set; }
    }
}