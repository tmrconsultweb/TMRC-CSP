using Microsoft.Store.PartnerCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    public interface IScenarioContext
    {
        /// <summary>
        /// Gets a partner operations instance which is user based authenticated.
        /// </summary>
        IAggregatePartner UserPartnerOperations { get; }
    }
}
