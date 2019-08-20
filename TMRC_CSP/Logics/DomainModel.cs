﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Logics
{
    public class DomainModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the domain is admin managed.
        /// </summary>
        public bool AdminManaged { get; set; }

        /// <summary>
        /// Gets or sets the type of the authentication.
        /// </summary>
        public string AuthenticationType { get; set; }

        /// <summary>
        /// Gets or sets the availability status.
        /// </summary>
        public string AvailabilityStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the domain is default.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the domain is initial.
        /// </summary>
        public bool IsInitial { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the domain is the root one.
        /// </summary>
        public bool IsRoot { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the domain has been verified.
        /// </summary>
        public bool IsVerified { get; set; }

        /// <summary>
        /// Gets or sets the name of the domain.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the supported services.
        /// </summary>
        public IEnumerable<string> SupportedServices { get; set; }
    }
}