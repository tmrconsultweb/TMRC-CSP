using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using TMRC_CSP.ViewModel.Telemetry;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    public class TelemetryService : DomainObject
    {
        /// <summary>
        /// An instance of the  appropriate telemetry provider for the portal.
        /// </summary>
        private static ITelemetryProvider telemetryProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetryService"/> class.
        /// </summary>
        /// <param name="applicationDomain">An application domain instance.</param>
        public TelemetryService(ApplicationDomain applicationDomain) : base(applicationDomain)
        {
        }

        /// <summary>
        /// Gets the instrumentation key from the portal configuration.
        /// </summary>
        public string InstrumentationKey { get; private set; }

        /// <summary>
        /// Gets an instance of the appropriate telemetry provider.
        /// </summary>
        public ITelemetryProvider Provider
        {
            get
            {
                if (string.IsNullOrEmpty(InstrumentationKey))
                {
                    telemetryProvider = new EmptyTelemetryProvider();
                }
                else
                {
                    telemetryProvider = new ApplicationInsightsTelemetryProvider();
                }

                return telemetryProvider;
            }
        }

        /// <summary>
        /// Initializes the telemetry provider based upon the portal configuration.
        /// </summary>
        /// <returns>A task for asynchronous purposes.</returns>
        public async Task InitializeAsync()
        {
            InstrumentationKey = null; //(await ApplicationDomain.Instance.PortalBranding.RetrieveAsync().ConfigureAwait(false)).InstrumentationKey;

            if (!string.IsNullOrEmpty(InstrumentationKey))
            {
                Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration.Active.InstrumentationKey =
                    InstrumentationKey;
            }
        }
    }
}