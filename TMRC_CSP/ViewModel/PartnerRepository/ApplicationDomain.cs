using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Store.PartnerCenter;
using Microsoft.Store.PartnerCenter.Extensions;
using Microsoft.Store.PartnerCenter.RequestContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TMRC_CSP.Logics;
using TMRC_CSP.Models;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    public class ApplicationDomain 
    {
        /// <summary>
        /// Provides access to core services.
        /// </summary>
        private static IExplorerProvider provider;


        /// <summary>
        /// Prevents a default instance of the <see cref="ApplicationDomain"/> class from being created.
        /// </summary>
        private ApplicationDomain()
        {
        }

        /// <summary>
        /// Gets an instance of the application domain for App.
        /// </summary>
        public static ApplicationDomain Instance { get; private set; }


        /// <summary>
        /// Gets an instance of the application domain for User.
        /// </summary>
        public static ApplicationDomain UserInstance { get; private set; }

        /// <summary>
        /// Gets a Partner Center SDK client.
        /// </summary>
        public IAggregatePartner PartnerCenterClient { get; private set; }

        /// <summary>
        /// Gets a Partner Center user.
        /// </summary>
        public IAggregatePartner userCenterClient { get; private set; }

        /// <summary>
        /// Gets the partner offers repository.
        /// </summary>
        public PartnerOffersRepository OffersRepository { get; private set; }

        /// <summary>
        /// Gets the Azure storage service.
        /// </summary>
        public AzureStorageService AzureStorageService { get; private set; }

        ///// <summary>
        ///// Gets the caching service.
        ///// </summary>
        //public CachingService CachingService { get; private set; }

        /// <summary>
        /// Gets the Microsoft offer logo indexer.
        /// </summary>
        public static MicrosoftOfferLogoIndexer MicrosoftOfferLogoIndexer { get; private set; }

        /// <summary>
        /// gets the portal branding service.
        /// </summary>
        public PortalBranding PortalBranding { get; private set; }

        /// <summary>
        /// Gets the portal localization service.
        /// </summary>
        public PortalLocalization PortalLocalization { get; private set; }

        ///// <summary>
        ///// Gets the portal payment configuration repository.
        ///// </summary>
        //public PaymentConfigurationRepository PaymentConfigurationRepository { get; private set; }

        ///// <summary>
        ///// Gets the portal PreApprovedCustomers configuration repository.
        ///// </summary>
        //public PreApprovedCustomersRepository PreApprovedCustomersRepository { get; private set; }

        /// <summary>
        /// Gets the customer subscriptions repository.
        /// </summary>
       // public CustomerSubscriptionsRepository CustomerSubscriptionsRepository { get; private set; }

        /// <summary>
        /// Gets the customer purchases repository.
        /// </summary>
       // public CustomerPurchasesRepository CustomerPurchasesRepository { get; private set; }

        ///// <summary>
        ///// Gets the customer orders repository.
        ///// </summary>
        //public OrdersRepository CustomerOrdersRepository { get; private set; }

        ///// <summary>
        ///// Gets the portal telemetry service.
        ///// </summary>
        public TelemetryService TelemetryService { get; private set; }

        ///// <summary>
        ///// Gets the customer registration repository.
        ///// </summary>
        //public CustomerRegistrationRepository CustomerRegistrationRepository { get; private set; }

        /// <summary>
        /// Initializes the core application domain objects.
        /// </summary>
        /// <returns>A task.</returns>
        public static async Task BootstrapAsync()
        {
            if (Instance == null)
            {
                Instance = new ApplicationDomain
                {
                    PartnerCenterClient = await AcquirePartnerCenterAccessAsync().ConfigureAwait(false),
                };

                Instance.PortalLocalization = new PortalLocalization(Instance);

                await Instance.PortalLocalization.InitializeAsync().ConfigureAwait(false);
            }
        }


        //public static async Task UserAsync()
        //{
        //    if (UserInstance == null)
        //    {
        //        UserInstance = new ApplicationDomain
        //        {
        //            userCenterClient = await AcquireUserCenterAccessAsync().ConfigureAwait(false),
        //        };

        //        UserInstance.PortalLocalization = new PortalLocalization(UserInstance);

        //        await UserInstance.PortalLocalization.InitializeAsync().ConfigureAwait(false);
        //    }
        //}


        /// <summary>
        /// Initializes the application domain objects.
        /// </summary>
        /// <returns>A task.</returns>
        public static async Task InitializeAsync()
        {
            if (Instance != null)
            {
               // Instance.AzureStorageService = new AzureStorageService(ApplicationConfiguration.AzureStorageConnectionString, ApplicationConfiguration.AzureStorageConnectionEndpointSuffix);
                //Instance.CachingService = new CachingService(Instance, ApplicationConfiguration.CacheConnectionString);
                Instance.OffersRepository = new PartnerOffersRepository(Instance);
                //Instance.MicrosoftOfferLogoIndexer = new MicrosoftOfferLogoIndexer(Instance);
                Instance.PortalBranding = new PortalBranding(Instance);
                //Instance.PaymentConfigurationRepository = new PaymentConfigurationRepository(Instance);
                //Instance.PreApprovedCustomersRepository = new PreApprovedCustomersRepository(Instance);
                //Instance.CustomerSubscriptionsRepository = new CustomerSubscriptionsRepository(Instance);
                //Instance.CustomerPurchasesRepository = new CustomerPurchasesRepository(Instance);
                //Instance.CustomerOrdersRepository = new OrdersRepository(Instance);
                //Instance.TelemetryService = new TelemetryService(Instance);
                //Instance.CustomerRegistrationRepository = new CustomerRegistrationRepository(Instance);

                //await Instance.TelemetryService.InitializeAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Authenticates with the Partner Center APIs.
        /// </summary>
        /// <returns>A Partner Center API client.</returns>
        private static async Task<IAggregatePartner> AcquirePartnerCenterAccessAsync()
        {
            PartnerService.Instance.ApiRootUrl = System.Configuration.ConfigurationManager.AppSettings["partnerCenter.apiEndPoint"];
            PartnerService.Instance.ApplicationName = "Web Store Front V1.6";

            IPartnerCredentials credentials = await PartnerCredentials.Instance.GenerateByApplicationCredentialsAsync(
               System.Configuration.ConfigurationManager.AppSettings["partnercenter.applicationId"],
               System.Configuration.ConfigurationManager.AppSettings["partnercenter.applicationSecret"],
                 System.Configuration.ConfigurationManager.AppSettings["partnercenter.AadTenantId"],
                 System.Configuration.ConfigurationManager.AppSettings["aadEndpoint"],
                 System.Configuration.ConfigurationManager.AppSettings["aadGraphEndpoint"]).ConfigureAwait(false);

            return PartnerService.Instance.CreatePartnerOperations(credentials);
        }



        /// <summary>
        /// Authenticates with the Partner Center APIs user.
        /// </summary>
        /// <returns>A Partner Center API client User.</returns>
        //private static async Task<IAggregatePartner> AcquireUserCenterAccessAsync()
        //{
        //    PartnerService.Instance.ApiRootUrl = System.Configuration.ConfigurationManager.AppSettings["partnerCenter.apiEndPoint"];
        //    PartnerService.Instance.ApplicationName = "Web Store Front V1.6";
        //    IPartnerCredentials credentials=null;
        //    //IPartnerCredentials credentials = await PartnerCredentials.Instance.GenerateByUserCredentialsAsync(
        //    //   System.Configuration.ConfigurationManager.AppSettings["User.ApplicationId"],

        //    AuthenticationResult aadAuthenticationResult = LoginUserToAad();


        //    AuthenticationResult token = await provider.AccessToken.GetAccessTokenAsync(
        //        $"{provider.Configuration.ActiveDirectoryEndpoint}/{provider.Configuration.PartnerCenterAccountId}",
        //        provider.Configuration.PartnerCenterEndpoint,
        //        new Models.ApplicationCredential
        //        {
        //            ApplicationId = provider.Configuration.ApplicationId,
        //            ApplicationSecret = provider.Configuration.ApplicationSecret,
        //            UseCache = true
        //        },
        //        provider.AccessToken.UserAssertionToken).ConfigureAwait(false);


        //    // Authenticate by user context with the partner service
        //    IPartnerCredentials userCredentials = PartnerCredentials.Instance.GenerateByUserCredentials(
        //        Configuration.UserAuthentication.ApplicationId,
        //        new AuthenticationToken(token.AccessToken, token.ExpiresOn));


        //            //System.Configuration.ConfigurationManager.AppSettings["partnercenter.applicationSecret"],
        //            //System.Configuration.ConfigurationManager.AppSettings["partnercenter.AadTenantId"],
        //            //     System.Configuration.ConfigurationManager.AppSettings["aadEndpoint"],
        //            //     System.Configuration.ConfigurationManager.AppSettings["aadGraphEndpoint"]).ConfigureAwait(false);

        //            return PartnerService.Instance.CreatePartnerOperations(credentials);
        //}


        public static async Task<IPartner> GetUserOperationsAsync(Guid correlationId)
        {
            //AuthenticationResult token = await provider.AccessToken.GetAccessTokenAsync(
            //   $"{System.Configuration.ConfigurationManager.AppSettings["aadEndpoint"].ToString()}/{System.Configuration.ConfigurationManager.AppSettings["partnercenter.AadTenantId"]}",
            //   System.Configuration.ConfigurationManager.AppSettings["partnerCenter.apiEndPoint"],
            //   new Models.ApplicationCredential
            //   {
            //       ApplicationId = System.Configuration.ConfigurationManager.AppSettings["partnercenter.applicationId"],
            //       ApplicationSecret = System.Configuration.ConfigurationManager.AppSettings["partnercenter.applicationSecret"],
            //       UseCache = true
            //   },
            //   provider.AccessToken.UserAssertionToken).ConfigureAwait(false);

            AuthenticationResult token = await provider.AccessToken.GetAccessTokenAsync(
                $"{provider.Configuration.ActiveDirectoryEndpoint}/{provider.Configuration.PartnerCenterAccountId}",
                provider.Configuration.PartnerCenterEndpoint,
                new Models.ApplicationCredential
                {
                    ApplicationId = provider.Configuration.ApplicationId,
                    ApplicationSecret = provider.Configuration.ApplicationSecret,
                    UseCache = true
                },
                provider.AccessToken.UserAssertionToken).ConfigureAwait(false);

            IPartnerCredentials credentials = await PartnerCredentials.Instance.GenerateByUserCredentialsAsync(
                provider.Configuration.ApplicationId,
                new AuthenticationToken(token.AccessToken, token.ExpiresOn)).ConfigureAwait(false);

            IAggregatePartner userOperations = PartnerService.Instance.CreatePartnerOperations(credentials);

            return userOperations.With(RequestContextFactory.Instance.Create(correlationId));
        }
    }
}