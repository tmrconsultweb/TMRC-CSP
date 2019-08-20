using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Store.PartnerCenter.Models.Customers;
using System.Globalization;
using Microsoft.Store.PartnerCenter.Models;
using Microsoft.Store.PartnerCenter;
using Microsoft.Store.PartnerCenter.RequestContext;
using Microsoft.Store.PartnerCenter.Models.Query;
using TMRC_CSP.ViewModel.PartnerRepository;
using TMRC_CSP.Logics;
using Microsoft.Store.PartnerCenter.Models.Users;
using System.Threading.Tasks;
using System.Security.Claims;
using TMRC_CSP.Controllers;

namespace TMRC_CSP.ViewModel.Users
{
    public class Users : BaseController //: DomainObject
    {
        public Users(IExplorerProvider provider) : base(provider)
        {
        }

        public async Task<IEnumerable<CustomerUser>> Get(string CustomerId)
        {
            CustomerId.AssertNotEmpty(nameof(CustomerId));

            CustomerPrincipal principal;
            CustomerUser user;
            Guid correlationId;
            IPartner operations;
            IExplorerProvider provider;

            correlationId = Guid.NewGuid();

            operations = await ApplicationDomain.GetUserOperationsAsync(correlationId).ConfigureAwait(false);


            // get customer users collection
            //var customerUsers = ApplicationDomain.userCenterClient.Customers.ById(CustomerId).Users.Get();
            principal = new CustomerPrincipal(ClaimsPrincipal.Current);

            if (
                //principal.CustomerId.Equals(provider.Configuration.PartnerCenterAccountId, StringComparison.InvariantCultureIgnoreCase) ||
                principal.CustomerId.Equals(CustomerId, StringComparison.InvariantCultureIgnoreCase))
            {
                //var customer = await ApplicationDomain.userCenterClient.Customers.ById(CustomerId).Users.GetAsync().ConfigureAwait(false);
                 var customer = await operations.Customers.ById(CustomerId).Users.GetAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}