using Microsoft.Store.PartnerCenter;
using Microsoft.Store.PartnerCenter.RequestContext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TMRC_CSP.Models;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    public class PartnerRepository : BasePartnerScenario
    {
        /// <summary>
        /// The Microsoft offers key in the cache.
        /// </summary>
        private const string MicrosoftOffersCacheKey = "MicrosoftOffers";

        /// <summary>
        /// The partner offers key in the cache.
        /// </summary>
        private const string PartnerOffersCacheKey = "PartnerOffers";

        /// <summary>
        /// The Azure BLOB name for the partner offers.
        /// </summary>
        private const string PartnerOffersBlobName = "partneroffers";


        //public async Task<IEnumerable<Models.PartnerOffer>> RetrieveAsync()
        //{
        //    //List<Models.PartnerOffer> partnerOffers = await ApplicationDomain.CachingService
        //    //    .FetchAsync<List<Models.PartnerOffer>>(PartnerOffersCacheKey).ConfigureAwait(false);

        //    //if (partnerOffers == null)
        //    //{

        //    List<Models.PartnerOffer> partnerOffers = new List<Models.PartnerOffer>();
        //    //CloudBlockBlob partnerOffersBlob = await GetPartnerOffersBlobAsync().ConfigureAwait(false);

        //    //if (await partnerOffersBlob.ExistsAsync().ConfigureAwait(false))
        //    //{
        //        // download the partner offer BLOB
        //        MemoryStream partnerOffersStream = new MemoryStream();
        //       // await partnerOffersBlob.DownloadToStreamAsync(partnerOffersStream).ConfigureAwait(false);
        //        partnerOffersStream.Seek(0, SeekOrigin.Begin);

        //        // deserialize the BLOB into a list of Partner offer objects
        //        partnerOffers =
        //            JsonConvert.DeserializeObject<List<Models.PartnerOffer>>(await new StreamReader(partnerOffersStream).ReadToEndAsync().ConfigureAwait(false));

        //        if (partnerOffers != null && partnerOffers.Count > 0)
        //        {
        //            // apply business rules to the offers
        //            PartnerOfferNormalizer offerNormalizer = new PartnerOfferNormalizer();

        //            foreach (Models.PartnerOffer partnerOffer in partnerOffers)
        //            {
        //                offerNormalizer.Normalize(partnerOffer);
        //            }
        //        }
        //    //}

        //    partnerOffers = partnerOffers ?? new List<Models.PartnerOffer>();

        //    // cache the partner offers
        //    //await ApplicationDomain.CachingService.StoreAsync(
        //    //    PartnerOffersCacheKey,
        //    //    partnerOffers).ConfigureAwait(false);
        //    //}

        //    return partnerOffers;
        //}


        /// <summary>
        /// Initializes a new instance of the <see cref="GetOffers"/> class.
        /// </summary>
        /// <param name="context">The scenario context.</param>


        public PartnerRepository(ScenarioContext context) : base("Get offers", context)
        {
            //this.Context = context;
        }

        public void Offers(ScenarioContext context)
        {
            //IAggregatePartner partnerOperations;
            IAggregatePartner partnerOperations = context.UserPartnerOperations;
            var offers = partnerOperations.Offers.ByCountry("US").Get();
        }


        //public List<MicrosoftOffer> RetrieveMicrosoftOffers()
        //{
        //    //List<MicrosoftOffer> microsoftOffers = await ApplicationDomain.CachingService
        //    //    .FetchAsync<List<MicrosoftOffer>>(MicrosoftOffersCacheKey).ConfigureAwait(false);

        //    //if (microsoftOffers == null)
        //    //{
        //    // Need to manage this based on the offer locale supported by the Offer API. Either its english or using one of the supported offer locale to retrieve localized offers for the store front.
        //    IPartner localeSpecificPartnerCenterClient = ApplicationDomain.PartnerCenterClient.With(RequestContextFactory.Instance.Create(ApplicationDomain.PortalLocalization.OfferLocale));

        //    // Offers.ByCountry is required to pull country / region specific offers. 
        //    Microsoft.Store.PartnerCenter.Models.ResourceCollection<Microsoft.Store.PartnerCenter.Models.Offers.Offer> partnerCenterOffers = localeSpecificPartnerCenterClient.Offers.ByCountry(ApplicationDomain.PortalLocalization.CountryIso2Code).Get();

        //    IEnumerable<Microsoft.Store.PartnerCenter.Models.Offers.Offer> eligibleOffers = partnerCenterOffers?.Items.Where(offer =>
        //        !offer.IsAddOn &&
        //        (offer.PrerequisiteOffers == null || !offer.PrerequisiteOffers.Any())
        //        && offer.IsAvailableForPurchase);

        //    List<MicrosoftOffer> microsoftOffers = new List<MicrosoftOffer>();

        //    if (eligibleOffers != null)
        //    {
        //        foreach (Microsoft.Store.PartnerCenter.Models.Offers.Offer partnerCenterOffer in eligibleOffers)
        //        {
        //            microsoftOffers.Add(new MicrosoftOffer()
        //            {
        //                Offer = partnerCenterOffer,
        //                // ThumbnailUri = new Uri(ApplicationDomain.MicrosoftOfferLogoIndexer.GetOfferLogoUriAsync(partnerCenterOffer), UriKind.Relative)
        //            });
        //        }
        //    }

        //    // cache the Microsoft offers for one day
        //    //await ApplicationDomain.CachingService.StoreAsync(
        //    //    MicrosoftOffersCacheKey,
        //    //    microsoftOffers,
        //    //    TimeSpan.FromDays(1)).ConfigureAwait(false);
        //    // }

        //    return microsoftOffers;
        //}
    
    }
}