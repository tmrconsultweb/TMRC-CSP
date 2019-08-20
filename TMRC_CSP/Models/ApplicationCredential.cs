using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;


namespace TMRC_CSP.Models
{
   
    /// <summary>
    /// Represents credentials to be used to acquire an access token.
    /// </summary>
    public sealed class ApplicationCredential
    {
        /// <summary>
        /// Identifier of the client requesting the token.
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// Secret of the client requesting the token.  
        /// </summary>
        public SecureString ApplicationSecret { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether or not distributed token cache 
        /// should be utilized for token acquistion. 
        /// </summary>
        public bool UseCache { get; set; }
    }
}