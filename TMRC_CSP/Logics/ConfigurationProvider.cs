using System.Configuration;
using System.Security;
using System.Threading.Tasks;
using TMRC_CSP.Logics;
using TMRC_CSP.ViewModel.PartnerRepository;

namespace TMRC_CSP.Logics
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        /// <summary>
        /// Provides access to core explorer providers.
        /// </summary>
        private readonly IExplorerProvider service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationProvider"/> class.
        /// </summary>
        /// <param name="service">Provides access to core explorer providers.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="service"/> is null.
        /// </exception>
        public ConfigurationProvider(IExplorerProvider service)
        {
            service.AssertNotNull(nameof(service));
            this.service = service;
        }

        /// <summary>
        /// Gets the Active Directory endpoint address.
        /// </summary>
        public string ActiveDirectoryEndpoint { get; private set; }

        /// <summary>
        /// Gets the application identifier value.
        /// </summary>
        public string ApplicationId { get; private set; }

        /// <summary>
        /// Gets the application secret value.
        /// </summary>
        public SecureString ApplicationSecret { get; private set; }

        /// <summary>
        /// Gets the application tenant identifier.
        /// </summary>
        public string ApplicationTenantId { get; private set; }

        /// <summary>
        /// Gets the Azure Resource Manager endpoint address.
        /// </summary>
        public string AzureResourceManagerEndpoint { get; private set; }

        /// <summary>
        /// Gets the Microsoft Graph endpoint address.
        /// </summary>
        public string GraphEndpoint { get; private set; }

        /// <summary>
        /// Gets the Application Insights instrumentation key.
        /// </summary>
        public string InstrumentationKey { get; private set; }

        /// <summary>
        /// Gets the endpoint address for the instance of Key Vault.
        /// </summary>
        public string KeyVaultEndpoint { get; private set; }

        /// <summary>
        /// Gets the Office 365 Management endpoint address.
        /// </summary>
        public string OfficeManagementEndpoint { get; private set; }

        /// <summary>
        /// Gets the Partner Center application tenant identifier.
        /// </summary>
        public string PartnerCenterAccountId { get; private set; }

        /// <summary>
        /// Gets the Partner Center application identifier.
        /// </summary>
        public string PartnerCenterApplicationId { get; private set; }

        /// <summary>
        /// Gets the Partner Center application secret.
        /// </summary>
        public SecureString PartnerCenterApplicationSecret { get; private set; }

        /// <summary>
        /// Gets the Partner Center endpoint address.
        /// </summary>
        public string PartnerCenterEndpoint { get; private set; }

        /// <summary>
        /// Gets the Redis Cache connection string.
        /// </summary>
        public SecureString RedisCacheConnectionString { get; private set; }

        /// <summary>
        /// Performs the necessary initialization operations.
        /// </summary>
        /// <returns>An instance of the  <see cref="Task"/> class that represents the asynchronous operation.</returns>
        public async Task InitializeAsync()
        {
            ActiveDirectoryEndpoint = System.Configuration.ConfigurationManager.AppSettings["ActiveDirectoryEndpoint"];
            ApplicationId = System.Configuration.ConfigurationManager.AppSettings["ApplicationId"];
            ApplicationTenantId = System.Configuration.ConfigurationManager.AppSettings["ApplicationTenantId"];
            AzureResourceManagerEndpoint = System.Configuration.ConfigurationManager.AppSettings["AzureResourceManagerEndpoint"];
            GraphEndpoint = System.Configuration.ConfigurationManager.AppSettings["GraphEndpoint"];
            InstrumentationKey = System.Configuration.ConfigurationManager.AppSettings["InstrumentationKey"];
            KeyVaultEndpoint = System.Configuration.ConfigurationManager.AppSettings["KeyVaultEndpoint"];
            OfficeManagementEndpoint = System.Configuration.ConfigurationManager.AppSettings["OfficeManagementEndpoint"];
            PartnerCenterAccountId = System.Configuration.ConfigurationManager.AppSettings["PartnerCenterAccountId"];
            PartnerCenterApplicationId = System.Configuration.ConfigurationManager.AppSettings["PartnerCenterApplicationId"];
            PartnerCenterEndpoint = System.Configuration.ConfigurationManager.AppSettings["PartnerCenterEndpoint"];

            ApplicationSecret = await service.Vault.GetAsync("ApplicationSecret").ConfigureAwait(false);
            PartnerCenterApplicationSecret = await service.Vault.GetAsync("PartnerCenterApplicationSecret").ConfigureAwait(false);
            RedisCacheConnectionString = await service.Vault.GetAsync("RedisCacheConnectionString").ConfigureAwait(false);
        }
    }
}