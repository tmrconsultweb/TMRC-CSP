﻿using System.Security.Claims;
namespace TMRC_CSP.Logics
{
    public class CustomerPrincipal : ClaimsPrincipal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerPrincipal"/> class.
        /// </summary>
        public CustomerPrincipal()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerPrincipal"/> class.
        /// </summary>
        /// <param name="principal">A user claims principal created by Azure Active Directory.</param>
        public CustomerPrincipal(ClaimsPrincipal principal) : base(principal)
        {
            CustomerId = principal.FindFirst("CustomerId")?.Value;
            Email = principal.FindFirst(ClaimTypes.Email)?.Value;
            Name = principal.FindFirst(ClaimTypes.Name)?.Value;
        }

        /// <summary>
        /// Gets the customer identifier for the authenticated user.
        /// </summary>
        public string CustomerId { get; }

        /// <summary>
        /// Gets the email address for the authenticated user.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Gets the name for the authenticated user. 
        /// </summary>
        public string Name { get; }
    }
}