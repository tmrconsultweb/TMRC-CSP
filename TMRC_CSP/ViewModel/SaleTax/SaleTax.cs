using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.ViewModel.SaleTax
{
    public class SaleTax
    {
        public string Save(Models.SaleTax _s, int ResellerId = 0)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();

                if (_s.Id != 0 && db.SaleTax.Any(m => m.Id == _s.Id))  //Update
                {
                    var saletax = db.SaleTax.Where(m => m.Id == _s.Id).SingleOrDefault();
                    saletax.Amount = _s.Amount;
                    saletax.ResellerId = ResellerId;
                    saletax.Country = _s.Country;
                    saletax.State = _s.State;
                }
                else  //Save
                {
                    Models.SaleTax saletax = new Models.SaleTax
                    {
                        Amount = _s.Amount,
                        Status = true,
                        ResellerId = ResellerId,
                        Country = _s.Country,
                        State = _s.State,
                    };
                    db.SaleTax.Add(saletax);
                }
                db.SaveChanges();
                return "Successully sale tax has been saved.";
            }
            catch (Exception ex)
            {
                return "Unknown error occur, Please try again.";
            }
        }

        public IEnumerable<Models.SaleTaxRepo> Get(int ResellerId = 0)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();

                var data = (from s in db.SaleTax
                            join c in db.Countries
                            on s.Country equals c.Code
                            where s.Status == true && c.Status == true
                            select new
                            {
                                s.Amount,
                                s.State,
                                Country = c.CountryName,
                                CountryCode = c.Code,
                                s.Id
                            }).OrderBy(m=>m.Country).OrderBy(m=>m.State);
                IEnumerable<Models.SaleTaxRepo> list = data.ToList().Select(r => new Models.SaleTaxRepo
                {
                    Amount = r.Amount,
                    Country = r.Country,
                    CountryCode = r.CountryCode,
                    State = r.State,
                    SaleTaxId = r.Id
                });
                return list;
            }
            catch
            {
                return new List<Models.SaleTaxRepo>();
            }
        }

        public Models.SaleTax Get(string Country, string state, int ResellerId = 0)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();

                var data = (from s in db.SaleTax
                            join c in db.Countries
                            on s.Country equals c.Code
                            where s.Status == true && c.Status == true &&
                            s.Country == Country && s.State.ToLower() == state.ToLower()
                            select new
                            {
                                s.Amount,
                                s.State,
                                Country = c.CountryName,
                                CountryCode=c.Code,
                                s.Id
                            }).SingleOrDefault();
                Models.SaleTax list = new Models.SaleTax
                {
                    Amount = data.Amount,
                    Country = data.Country,
                    State = data.State,
                    Id = data.Id
                };
                return list;
            }
            catch
            {
                return new Models.SaleTax();
            }
        }

    }
}