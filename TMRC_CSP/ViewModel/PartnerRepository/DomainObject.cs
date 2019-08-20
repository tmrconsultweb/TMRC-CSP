﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    /// <summary>
    /// The base class for all domain objects.
    /// </summary>
    public abstract class DomainObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainObject"/> class.
        /// </summary>
        /// <param name="applicationDomain">An application domain instance.</param>
        protected DomainObject(ApplicationDomain applicationDomain)
        {
            //applicationDomain.AssertNotNull(nameof(applicationDomain));
            this.ApplicationDomain = applicationDomain;
        }

        /// <summary>
        /// Gets the application domain instance hosting all domain services.
        /// </summary>
        protected ApplicationDomain ApplicationDomain { get; private set; }
    }
}