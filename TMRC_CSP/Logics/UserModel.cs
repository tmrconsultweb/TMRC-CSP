using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Logics
{
    public class UserModel
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the last directory synchronization time.
        /// </summary>
        public DateTime? LastDirectorySyncTime { get; set; }

        /// <summary>
        /// Gets or sets the usage location.
        /// </summary>
        public string UsageLocation { get; set; }

        /// <summary>
        /// Gets or sets the user principal name.
        /// </summary>
        public string UserPrincipalName { get; set; }
    }
}