using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace TMRC_CSP.Logics
{
    public interface IGraphClient
    {
        /// <summary>
        /// Gets a list of roles assigned to the specified object identifier.
        /// </summary>
        /// <param name="objectId">Object identifier for the object to be checked.</param>
        /// <returns>A list of roles that that are associated with the specified object identifier.</returns>
        Task<List<RoleModel>> GetDirectoryRolesAsync(string objectId);

        /// <summary>
        /// Gets the service configuration records for the specified domain. 
        /// </summary>
        /// <param name="domain">Name of the domain</param>
        /// <returns>A list of service configuration records for the specified domain.</returns>
        Task<List<DomainDnsRecord>> GetDomainConfigurationRecordsAsync(string domain);

        /// <summary>
        /// Gets a list of domains configured for the customer.
        /// </summary>
        /// <returns>A list of domains configured for the customer.</returns>
        Task<List<DomainModel>> GetDomainsAsync();

        /// <summary>
        /// Gets a list of users for the customer.
        /// </summary>
        /// <returns>A list of users that belong to the customer.</returns>
        Task<List<UserModel>> GetUsersAsync();
    }
}
