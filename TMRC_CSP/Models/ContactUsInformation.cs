using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    /// <summary>
    /// Holds contact us information.
    /// </summary>
    public class ContactUsInformation
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string Phone { get; set; }
    }
}