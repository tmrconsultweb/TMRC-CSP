using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TMRC_CSP.ViewModel.OffersSetup
{
    public class OffersSetup
    {
        public void AddOffersSetup(Models.OffersSetup OffersSetup)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();

                if(!db.OffersSetup.Any(m=>m.MicrosoftOfferId == OffersSetup.MicrosoftOfferId))
                {
                    OffersSetup.CreatedDate = DateTime.Now;
                    OffersSetup.AppliedDate = DateTime.Now;
                    OffersSetup.Status = true;
                    db.OffersSetup.Add(OffersSetup);
                }
                else
                {
                    var _o = db.OffersSetup.Where(m => m.MicrosoftOfferId == OffersSetup.MicrosoftOfferId).SingleOrDefault();
                    _o.Features = OffersSetup.Features;
                    _o.Price = OffersSetup.Price;
                    _o.SubTitle = OffersSetup.SubTitle;
                    _o.Summary = OffersSetup.Summary;
                    _o.Title = OffersSetup.Title;
                }
                
                db.SaveChanges();
            }
            catch
            {

            }
            
        }

        public List<Models.OffersSetup> GetOffers()
        {
            var db = new Context.ConnectionStringsContext();
            return db.OffersSetup.Where(m => m.Status == true).ToList();
        }

        public async Task<List<Models.PartnerAndMicrosoftRepository>> RecieveOffersDetail()
        {
            OffersSetup _of = new OffersSetup();
            List<Models.OffersSetup> _o = new List<Models.OffersSetup>();
            _o = _of.GetOffers();

            List<Models.PartnerAndMicrosoftRepository> _p = new List<Models.PartnerAndMicrosoftRepository>();

            foreach (Models.OffersSetup _d in _o)
            {
                Models.PartnerAndMicrosoftRepository p = new Models.PartnerAndMicrosoftRepository();
                p.OffersSetup = _d;
                p.MicrosoftOffer = await PartnerRepository.ApplicationDomain.Instance.OffersRepository.RetrieveMicrosoftOfferByIdAsync(_d.MicrosoftOfferId).ConfigureAwait(false);
                _p.Add(p);
            }
            return _p;
        }

        public bool DeleteOffer(string _p)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                if (db.OffersSetup.Any(m => m.MicrosoftOfferId == _p))
                {
                    var _o = db.OffersSetup.Where(m => m.MicrosoftOfferId == _p).SingleOrDefault();
                    _o.Status = false;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public Models.OffersSetup GetOfferByMicrosoftOfferId(string Id)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                if (db.OffersSetup.Any(m => m.MicrosoftOfferId == Id))
                    return db.OffersSetup.Where(m => m.MicrosoftOfferId == Id).SingleOrDefault();
                else
                    return new Models.OffersSetup();
            }
            catch
            {
                return new Models.OffersSetup();
            }
        }
    }
}