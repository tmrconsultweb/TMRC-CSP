using Microsoft.Store.PartnerCenter;
using Microsoft.Store.PartnerCenter.RequestContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMRC_CSP.Models;
using TMRC_CSP.ViewModel.PartnerRepository;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    public class Offers : DomainObject
    {  
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerOffersRepository"/> class.
        /// </summary>
        /// <param name="applicationDomain">An application domain instance.</param>
        public Offers(ApplicationDomain applicationDomain) : base(applicationDomain)
        {
        }

        public List<MicrosoftOffer> RetrieveMicrosoftOffers()
        {
            //List<MicrosoftOffer> microsoftOffers = await ApplicationDomain.CachingService
            //    .FetchAsync<List<MicrosoftOffer>>(MicrosoftOffersCacheKey).ConfigureAwait(false);

            //if (microsoftOffers == null)
            //{
            // Need to manage this based on the offer locale supported by the Offer API. Either its english or using one of the supported offer locale to retrieve localized offers for the store front.
            IPartner localeSpecificPartnerCenterClient = ApplicationDomain.PartnerCenterClient.With(RequestContextFactory.Instance.Create(ApplicationDomain.PortalLocalization.OfferLocale));

            // Offers.ByCountry is required to pull country / region specific offers. 
            Microsoft.Store.PartnerCenter.Models.ResourceCollection<Microsoft.Store.PartnerCenter.Models.Offers.Offer> partnerCenterOffers = localeSpecificPartnerCenterClient.Offers.ByCountry(ApplicationDomain.PortalLocalization.CountryIso2Code).Get();

            IEnumerable<Microsoft.Store.PartnerCenter.Models.Offers.Offer> eligibleOffers = partnerCenterOffers?.Items.Where(offer =>
                !offer.IsAddOn &&
                (offer.PrerequisiteOffers == null || !offer.PrerequisiteOffers.Any())
                && offer.IsAvailableForPurchase);

            List<MicrosoftOffer> microsoftOffers = new List<MicrosoftOffer>();

            if (eligibleOffers != null)
            {
                foreach (Microsoft.Store.PartnerCenter.Models.Offers.Offer partnerCenterOffer in eligibleOffers)
                {
                    microsoftOffers.Add(new MicrosoftOffer()
                    {
                        Offer = partnerCenterOffer,
                        // ThumbnailUri = new Uri(ApplicationDomain.MicrosoftOfferLogoIndexer.GetOfferLogoUriAsync(partnerCenterOffer), UriKind.Relative)
                    });
                }
            }

            // cache the Microsoft offers for one day
            //await ApplicationDomain.CachingService.StoreAsync(
            //    MicrosoftOffersCacheKey,
            //    microsoftOffers,
            //    TimeSpan.FromDays(1)).ConfigureAwait(false);
            // }

            return microsoftOffers;
        }

        public void test()
        {

        }
    }
}