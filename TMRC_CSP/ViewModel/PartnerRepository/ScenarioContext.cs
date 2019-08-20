using Microsoft.Store.PartnerCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    public class ScenarioContext
    {
        /// <summary>
        /// Gets a configuration instance.
        /// </summary>
        public ConfigurationManager Configuration => ConfigurationManager.Instance;

        public IAggregatePartner UserPartnerOperations { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioContext"/> class.
        /// </summary>
        public ScenarioContext()
        {
            PartnerService.Instance.ApiRootUrl = this.Configuration.PartnerService.PartnerServiceApiEndpoint.ToString();
            PartnerService.Instance.ApplicationName = "Partner Center .NET SDK Samples";
        }
    }
}