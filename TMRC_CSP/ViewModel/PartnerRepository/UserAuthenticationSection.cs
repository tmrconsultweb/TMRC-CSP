using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    public class UserAuthenticationSection : Section
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthenticationSection"/> class.
        /// </summary>
        /// <param name="sectionName">The application authentication section name.</param>
        public UserAuthenticationSection(string sectionName) : base(sectionName)
        {
        }

        /// <summary>
        /// Gets the AAD application ID.
        /// </summary>
        public string ApplicationId
        {
            get
            {
                return this.ConfigurationSection["ApplicationId"];
            }
        }

        /// <summary>
        /// Gets the resource the application is attempting to access, i.e. the partner API service.
        /// </summary>
        public Uri ResourceUrl
        {
            get
            {
                return new Uri(this.ConfigurationSection["ResourceUrl"]);
            }
        }

        /// <summary>
        /// Gets the application redirect URL.
        /// </summary>
        public Uri RedirectUrl
        {
            get
            {
                return new Uri(this.ConfigurationSection["RedirectUrl"]);
            }
        }
    }
}