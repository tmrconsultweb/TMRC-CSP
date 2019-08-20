using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TMRC_CSP.Models;
using TMRC_CSP.ViewModel.PartnerRepository;

namespace TMRC_CSP.ViewModel.MicrosoftOfferAndPriceList
{
    public class MicrosoftOfferAndPriceList
    {
        //public async Task<Models.MicrosoftOfferAndPriceListRepo> GetOffers()
        //{
        //    Models.MicrosoftOfferAndPriceListRepo _repo = new MicrosoftOfferAndPriceListRepo();
        //    IEnumerable<MicrosoftOffer> microsoftOffers = await ApplicationDomain.Instance.OffersRepository.RetrieveMicrosoftOffersAsync().ConfigureAwait(false);
        //    List<Models.MicrosoftPriceList> _priceList = new List<Models.MicrosoftPriceList>();
        //    ViewModel.MicrosoftPriceList.PriceList pl = new MicrosoftPriceList.PriceList();
        //    _priceList = pl.Get(); 

        //    _repo.MicrosoftOffer = microsoftOffers;
        //    _repo.MicrosoftPriceList = _priceList;

            
        //    return _repo;
        //}


        public async Task<IEnumerable<Models.MicrosoftOffer>> GetOffers()
        {
            Models.MicrosoftOfferAndPriceListRepo _repo = new MicrosoftOfferAndPriceListRepo();
            IEnumerable<MicrosoftOffer> microsoftOffers = await ApplicationDomain.Instance.OffersRepository.RetrieveMicrosoftOffersAsync().ConfigureAwait(false);

            //List<Models.MicrosoftPriceList> _priceList = new List<Models.MicrosoftPriceList>();
            //ViewModel.MicrosoftPriceList.PriceList pl = new MicrosoftPriceList.PriceList();

            //ViewModel.ResellerPrice.ResellerPrice resellerPrice = new ResellerPrice.ResellerPrice();

            //_priceList = ResellerId == 0 ? pl.Get() : resellerPrice.GetPriceList(ResellerId);

            //_repo.MicrosoftOffer = microsoftOffers;
            //_repo.MicrosoftPriceList = _priceList;


            return microsoftOffers;
        }

        public List<Models.MicrosoftPriceList> GetPriceList()
        {
            ViewModel.MicrosoftPriceList.PriceList pl = new MicrosoftPriceList.PriceList();
            return pl.Get();
        }

       
    }
}