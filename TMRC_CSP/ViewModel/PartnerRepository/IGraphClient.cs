using System.Collections.Generic;
using System.Threading.Tasks;
using TMRC_CSP.Logics;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    public interface IGraphClient
    {
        /// <summary>
        /// Gets a list of roles assigned to the specified object identifier.
        /// </summary>
        /// <param name="objectId">Object identifier for the object to be checked.</param>
        /// <returns>A list of roles that that are associated with the specified object identifier.</returns>
        Task<List<RoleModel>> GetDirectoryRolesAsync(string objectId);
    }
}