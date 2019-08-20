using Microsoft.Store.PartnerCenter.Models;
using Microsoft.Store.PartnerCenter.Models.Agreements;
using Microsoft.Store.PartnerCenter.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using TMRC_CSP.Models;
using TMRC_CSP.ViewModel.PartnerRepository;

namespace TMRC_CSP.ViewModel.OffersSetup
{
    public class Agreements
    {


        public async Task<string> MicosoftCloudAgreement(Microsoft.Store.PartnerCenter.Models.Customers.Customer newCustomer)
        {
            //BrandingConfiguration branding = await ApplicationDomain.Instance.PortalBranding.RetrieveAsync().ConfigureAwait(false);


            ResourceCollection<AgreementMetaData> agreements = await ApplicationDomain.Instance.PartnerCenterClient.AgreementDetails.GetAsync().ConfigureAwait(false);

            // Obtain reference to the Microsoft Cloud Agreement.
            AgreementMetaData microsoftCloudAgreement = agreements.Items.FirstOrDefault(agr => agr.AgreementType == Microsoft.Store.PartnerCenter.Models.Agreements.AgreementType.MicrosoftCloudAgreement);

            // Attest that the customer has accepted the Microsoft Cloud Agreement (MCA).
            await ApplicationDomain.Instance.PartnerCenterClient.Customers[newCustomer.Id].Agreements.CreateAsync(
                new Agreement
                {
                    DateAgreed = DateTime.UtcNow,
                    PrimaryContact = new Microsoft.Store.PartnerCenter.Models.Agreements.Contact
                    {
                        Email = newCustomer.BillingProfile.Email,//customerRegistrationInfoPersisted.Email,
                        FirstName = newCustomer.BillingProfile.DefaultAddress.FirstName,//customerRegistrationInfoPersisted.FirstName,
                        LastName = newCustomer.BillingProfile.DefaultAddress.LastName,//customerRegistrationInfoPersisted.LastName,
                        PhoneNumber = newCustomer.BillingProfile.DefaultAddress.PhoneNumber,//customerRegistrationInfoPersisted.Phone
                    },
                    TemplateId = microsoftCloudAgreement.TemplateId,
                    Type = Microsoft.Store.PartnerCenter.Models.Agreements.AgreementType.MicrosoftCloudAgreement,
                    //Ahsan
                    UserId = "631c1b48-e58c-46d4-a947-e7cea8b3d796", //branding.AgreementUserId
                }).ConfigureAwait(false);
            return newCustomer.CompanyProfile.TenantId;
        }
    }
}