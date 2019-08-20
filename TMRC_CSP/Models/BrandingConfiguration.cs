﻿using System;
using System.IO;

namespace TMRC_CSP.Models
{
    /// <summary>
    /// Holds the website's branding configuration.
    /// </summary>
    public class BrandingConfiguration
    {
        /// <summary>
        /// Gets or sets the user identifier to be associated with the acceptance of the MCA.
        /// </summary>
        public string AgreementUserId { get; set; }

        /// <summary>
        /// Gets or sets the contact sales information.
        /// </summary>
        public ContactUsInformation ContactSales { get; set; }

        /// <summary>
        /// Gets or sets the contact us information.
        /// </summary>
        public ContactUsInformation ContactUs { get; set; }

        /// <summary>
        /// Gets or sets the header image.
        /// </summary>
        public Uri HeaderImage { get; set; }

        /// <summary>
        /// Gets or sets the binary content of the header image.
        /// </summary>
        public Stream HeaderImageContent { get; set; }

        /// <summary>
        /// Gets or sets the Application Insights instrumentation key.
        /// </summary>
        public string InstrumentationKey { get; set; }

        /// <summary>
        /// Gets or sets the organization name.
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// Gets or sets the organization logo.
        /// </summary>
        public Uri OrganizationLogo { get; set; }

        /// <summary>
        /// Gets or sets the binary content of the organization logo.
        /// </summary>
        public Stream OrganizationLogoContent { get; set; }

        /// <summary>
        /// Gets or sets a privacy link for using the portal.
        /// </summary>
        public Uri PrivacyAgreement { get; set; }
    }
}