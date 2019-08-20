    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMRC_CSP.ViewModel.Telemetry;

namespace TMRC_CSP.Logics
{
    public interface IExplorerProvider
    {
        /// <summary>
        /// Gets a reference to the token management service.
        /// </summary>
        IAccessTokenProvider AccessToken { get; }

        /// <summary>
        /// Gets the service that provides caching functionality.
        /// </summary>
        ICacheProvider Cache { get; }

        /// <summary>
        /// Gets a reference to the available configurations.
        /// </summary>
        IConfigurationProvider Configuration { get; }

        /// <summary>
        /// Gets a reference to the communication service.
        /// </summary>
        IHttpService Communication { get; }

        /// <summary>
        /// Gets a reference to the partner operations.
        /// </summary>
       IPartnerOperations PartnerOperations { get; }

        /// <summary>
        /// Gets the telemetry service reference.
        /// </summary>
        ITelemetryProvider Telemetry { get; }

        /// <summary>
        /// Gets a reference to the vault provider. 
        /// </summary>
        IVaultProvider Vault { get; }

        /// <summary>
        /// Initializes this instance of the <see cref="ReportProvider"/> class.
        /// </summary>
        /// <returns>An instance of <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task InitializeAsync();
    }
}
