using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TMRC_CSP.ViewModel.PartnerRepository;

namespace TMRC_CSP.ViewModel.ResellerRequest
{
    public class NewRequest
    {
        //Get all records whose is't verified and status is active
        public List<Models.Customers> GetAll()
        {
            var db = new Context.ConnectionStringsContext();
            return db.Customers.Where(m => m.IsVerified == false && m.Status == true).ToList();
        }

        //Delete the request reseller (Soft deletion)
        public bool DeleteRequest(int id)
        {
            var db = new Context.ConnectionStringsContext();
            try
            {
                db.Customers.Where(m => m.Id == id).SingleOrDefault().Status = false;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Accept the request reseller
        public async Task<string> AcceptRequest(TMRC_CSP.Models.Customers _resellerRequest, int ResellerId=0)
        {
            var db = new Context.ConnectionStringsContext();
            try
            {

                //var obj = (Models.Customers)HttpContext.Current.Session["_resellerRequest"];

                // TODO :: Loc. may need special handling for national clouds deployments (China).
                string domainName = string.Format(CultureInfo.InvariantCulture, "{0}.onmicrosoft.com", _resellerRequest.PrimaryDomain);

                // check domain available.

                bool isDomainTaken = await ApplicationDomain.Instance.PartnerCenterClient.Domains.ByDomain(domainName).ExistsAsync().ConfigureAwait(false);
                if (isDomainTaken)
                {
                    return "Domain name already exist";
                }

                // get the locale, we default to the first locale used in a country for now.
                Microsoft.Store.PartnerCenter.Models.CountryValidationRules.CountryValidationRules customerCountryValidationRules = await ApplicationDomain.Instance.PartnerCenterClient.CountryValidationRules.ByCountry(_resellerRequest.Country).GetAsync().ConfigureAwait(false);
                string billingCulture = customerCountryValidationRules.SupportedCulturesList.FirstOrDefault();      // default billing culture is the first supported culture for the customer's selected country. 
                string billingLanguage = customerCountryValidationRules.SupportedLanguagesList.FirstOrDefault();    // default billing culture is the first supported language for the customer's selected country. 

                //Add Customer to Partner center
                Customers cus = new Customers(ApplicationDomain.Instance);
                Microsoft.Store.PartnerCenter.Models.Customers.Customer newcustomer = cus.CreateCustomer(_resellerRequest, domainName, billingCulture, billingLanguage);

                Models.Customers res;
                if (db.Customers.Any(m => m.Id == _resellerRequest.Id))   //For Update
                {
                    res = db.Customers.Where(m => m.Id == _resellerRequest.Id).SingleOrDefault();
                    res.Address1 = _resellerRequest.Address1;
                    res.Address2 = _resellerRequest.Address2;
                    res.City = _resellerRequest.City;
                    res.Province = _resellerRequest.Province;
                    res.ZipCode = _resellerRequest.ZipCode;
                    res.Country = _resellerRequest.Country;
                    res.PhoneNumber = _resellerRequest.PhoneNumber;
                    res.FirstName = _resellerRequest.FirstName;
                    res.LastName = _resellerRequest.LastName;
                    res.Email = _resellerRequest.Email;
                    res.Company = _resellerRequest.Company;
                    res.MicrosoftId = newcustomer.Id;
                    res.PrimaryDomain = domainName;
                    res.VerifiedDate = DateTime.Now;
                    res.IsVerified = true;
                    res.IsAcceptTerms = _resellerRequest.IsAcceptTerms;

                }
                else   //For Adding
                {
                    res = new Models.Customers()
                    {
                        Address1 = _resellerRequest.Address1,
                        Address2 = _resellerRequest.Address2,
                        City = _resellerRequest.City,
                        Province = _resellerRequest.Province,
                        ZipCode = _resellerRequest.ZipCode,
                        Country = _resellerRequest.Country,
                        PhoneNumber = _resellerRequest.PhoneNumber,
                        //Language = customerViewModel.Language,
                        FirstName = _resellerRequest.FirstName,
                        LastName = _resellerRequest.LastName,
                        Email = _resellerRequest.Email,
                        Company = _resellerRequest.Company,
                        MicrosoftId = newcustomer.Id,
                        //UserName = customerViewModel.Email,
                        // BillingLanguage = billingLanguage,
                        // BillingCulture = billingCulture,
                        PrimaryDomain = domainName,
                        //DomainPrefix = customerViewModel.DomainPrefix,
                        VerifiedDate = DateTime.Now,
                        IsVerified = true,
                        IsAcceptTerms = _resellerRequest.IsAcceptTerms,
                        AdditionalInfo = "",
                        Status = true,
                        CreatedDate = DateTime.Now,
                        UserName= newcustomer.UserCredentials.UserName,
                        Password = newcustomer.UserCredentials.Password,
                        ResellerId = ResellerId
                    };
                    db.Customers.Add(res);
                }
                db.SaveChanges();
                ViewModel.OffersSetup.Agreements agr = new OffersSetup.Agreements();
                await agr.MicosoftCloudAgreement(newcustomer);
                return res.MicrosoftId;
            }
            catch
            {
                return "Unknown error occur";
            }
        }

        //Get the requested Data
        public Models.Customers GetReseller(int? id)
        {
            var db = new Context.ConnectionStringsContext();
            return db.Customers.Where(m => m.Id == id).SingleOrDefault();
        }

        // Update
    }
}