using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public enum CommerceOperationType
    {
        /// <summary>
        /// A brand new purchase.
        /// </summary>
        NewPurchase,

        /// <summary>
        /// Purchase of additional seats for an existing subscription.
        /// </summary>
        AdditionalSeatsPurchase,

        /// <summary>
        /// Existing subscription renewal.
        /// </summary>
        Renewal
    }
}