using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using TMRC_CSP. Logics;
using TMRC_CSP.Models;
using TMRC_CSP.ViewModel.PartnerRepository;

namespace TMRC_CSP.Logics
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        /// <summary>
        /// Name of the authentication header to be utilized. 
        /// </summary>
        private const string AuthHeaderName = "Authorization";

        /// <summary>
        /// The type of token being utilized for the authentication request.
        /// </summary>
        private const string TokenType = "Bearer";

        /// <summary>
        /// Provides access to core services.
        /// </summary>
        private readonly IExplorerProvider provider;

        /// <summary>
        /// The customer identifier utilized to scope the Microsoft Graph requests.
        /// </summary>
        private readonly string customerId;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationProvider"/> class.
        /// </summary>
        /// <param name="provider">Provides access to core services.</param>
        /// <param name="customerId">Identifier for customer whose resources are being accessed.</param>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="customerId"/> is empty or null.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="provider"/> is null.
        /// </exception>
        public AuthenticationProvider(IExplorerProvider provider, string customerId)
        {
            provider.AssertNotNull(nameof(provider));
            customerId.AssertNotEmpty(nameof(customerId));

            this.customerId = customerId;
            this.provider = provider;
        }

        /// <summary>
        /// Performs the necessary authentication and injects the required header.
        /// </summary>
        /// <param name="request">The request being made to the Microsoft Graph API.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            AuthenticationResult token = await provider.AccessToken.GetAccessTokenAsync(
                $"{provider.Configuration.ActiveDirectoryEndpoint}/{customerId}",
                provider.Configuration.GraphEndpoint,
                new ApplicationCredential
                {
                    ApplicationId = provider.Configuration.ApplicationId,
                    ApplicationSecret = provider.Configuration.ApplicationSecret,
                    UseCache = true
                }).ConfigureAwait(false);

            request.Headers.Add(AuthHeaderName, $"{TokenType} {token.AccessToken}");
        }
    }
}