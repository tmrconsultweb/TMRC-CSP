using Microsoft.Store.PartnerCenter.Models.Customers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.Store.PartnerCenter.Models;
using Microsoft.Store.PartnerCenter;
using Microsoft.Store.PartnerCenter.RequestContext;
using Microsoft.Store.PartnerCenter.Models.Query;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    public class Customers : DomainObject
    {
        public Customers(ApplicationDomain applicationDomain) : base(applicationDomain)
        {
        }
        public Customer CreateCustomer(Models.Customers res, string Primarydomain, string billingCulture, string billingLanguage)
        {
            try
            {
                var customerToCreate = new Customer()
                {
                    CompanyProfile = new CustomerCompanyProfile()
                    {
                        Domain = Primarydomain,
                    },
                    BillingProfile = new CustomerBillingProfile()
                    {
                        Culture = billingCulture, //"en-US",
                        Email = res.Email,
                        Language = billingLanguage, //"en",
                        CompanyName = res.Company,

                        DefaultAddress = new Address()
                        {
                            FirstName = res.FirstName,
                            LastName = res.LastName,
                            AddressLine1 = res.Address1,
                            AddressLine2 = res.Address2,
                            City = res.City,
                            State = res.Province, //"WA",
                            Country = res.Country, //"US",

                            PhoneNumber = res.PhoneNumber,
                            PostalCode = res.ZipCode
                        }
                    }
                };


                var newCustomer = ApplicationDomain.Instance.PartnerCenterClient.Customers.Create(customerToCreate);

                string MailBody = "<html><body>"
                    + "<b>Name</b>"
                    + res.FirstName + " " + res.LastName
                    + "<b>Email</b>"
                    + res.PrimaryDomain
                    + "<b>Password</b>"
                    + newCustomer.UserCredentials.Password
                    + "</body></html>";

                ViewModel.SendEmail.SendEmailRegister(res.Email, "Successfully Account has been created", MailBody);

                return newCustomer;
            }
            catch
            {
                throw new Exception("Unknown error occur, Please try again");
            }

        }


        public IEnumerable<Models.Customers> GetCustomerList()
        {
            // SeekBasedResourceCollection<Customer> list1= new SeekBasedResourceCollection<Customer>(;
            try
            {
                //// All the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
                //IPartner scopedPartnerOperations = ApplicationDomain.PartnerCenterClient.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

                //// read customers into chunks of 40s
                //var customersBatch = scopedPartnerOperations.Customers.Query(QueryFactory.Instance.BuildIndexedQuery(40));
                //var customersEnumerator = scopedPartnerOperations.Enumerators.Customers.Create(customersBatch);

                //Get Customer from API
                SeekBasedResourceCollection<Customer> list = ApplicationDomain.PartnerCenterClient.Customers.Get();

                //Get Reseller/Agent from db
                ViewModel.Resellers.Resellers resellers = new ViewModel.Resellers.Resellers();
                IEnumerable<Models.Reseller> reseller = resellers.GetActive();

                //Get Customers from db
                IEnumerable<Models.Customers> getcustomer = GetCustomers();


                List<Models.Customers> customer = new List<Models.Customers>();

                //foreach (var r in reseller)
                //{
                //    foreach (var i in getcustomer)
                //    {
                //        if (i.ResellerId == r.Id || i.ResellerId == 0)
                //        {
                //            foreach (var x in list.Items)
                //            {
                //                if (i.MicrosoftId == x.Id)
                //                {
                //                    Models.Customers cus = new Models.Customers()
                //                    {
                //                        ResellerId = i.Id,
                //                        PrimaryDomain = x.CompanyProfile.Domain,
                //                        MicrosoftId = x.Id,
                //                        Company = x.CompanyProfile.CompanyName,
                //                    };
                //                    customer.Add(cus);
                //                }
                //            }
                //        }
                //    }
                //}

                foreach (var x in list.Items.OrderBy(m => m.Id)) //Api
                {
                    var gc = getcustomer.Where(m => m.MicrosoftId == x.Id).SingleOrDefault();
                    if (gc != null)   // It customer is exit in my db
                    {
                        //    foreach (var i in gc)  // db customer
                        //    {
                        //        if (i.MicrosoftId == x.Id)
                        //        {
                        //var res = reseller.Where(m => m.Id == gc.ResellerId).SingleOrDefault();
                        if (gc.ResellerId == 0) // it means direct customer
                        {
                            Models.Customers cus = new Models.Customers()
                            {
                                ResellerId = 0,  // it means it's a direct customer
                                PrimaryDomain = x.CompanyProfile.Domain,
                                MicrosoftId = x.Id,
                                Company = x.CompanyProfile.CompanyName,
                            };
                            customer.Add(cus);
                        }
                        else  // it means indirect customer
                        {
                            //var res = reseller.Where(m => m.Id == gc.ResellerId).SingleOrDefault();
                            Models.Customers cus = new Models.Customers()
                            {
                                ResellerId = gc.ResellerId,
                                PrimaryDomain = x.CompanyProfile.Domain,
                                MicrosoftId = x.Id,
                                Company = x.CompanyProfile.CompanyName,
                            };
                            customer.Add(cus);
                        }
                        //        foreach (var r in reseller)  // db agent
                        //        {
                        //            if (i.ResellerId == r.Id || i.ResellerId == 0)
                        //            {

                        //            }
                        //        }
                        //    }

                        //}
                    }
                    else  // If customer is not exit in my db
                    {
                        Models.Customers cus = new Models.Customers()
                        {
                            ResellerId = 0,  // it means it's a direct customer
                            PrimaryDomain = x.CompanyProfile.Domain,
                            MicrosoftId = x.Id,
                            Company = x.CompanyProfile.CompanyName,
                        };
                        customer.Add(cus);
                    }

                }

                return customer;
            }
            catch
            {
                return null;
            }

        }

        public Models.Customers GetAPICustomerById(string CustomerId)
        {
            Customer list = ApplicationDomain.PartnerCenterClient.Customers.ById(CustomerId).Get();

            return new Models.Customers
            {
                Address1 = list.BillingProfile.DefaultAddress.AddressLine1,
                Address2 = list.BillingProfile.DefaultAddress.AddressLine2,
                City = list.BillingProfile.DefaultAddress.City,
                Country = list.BillingProfile.DefaultAddress.Country,
                FirstName = list.BillingProfile.DefaultAddress.FirstName,
                LastName = list.BillingProfile.DefaultAddress.LastName,
                Company = list.BillingProfile.CompanyName,
                Email = list.BillingProfile.Email,
                PhoneNumber = list.BillingProfile.DefaultAddress.PhoneNumber,
                ZipCode = list.BillingProfile.DefaultAddress.PostalCode,
                PrimaryDomain = list.CompanyProfile.Domain,
                Province = list.BillingProfile.DefaultAddress.State,
            };
        }

        //Get customers from db
        public IEnumerable<Models.Customers> GetCustomers()
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                return db.Customers.Where(m => m.Status == true).ToList();
            }
            catch
            {
                return new List<Models.Customers>();
            }
        }



        //Get customerby id from db
        public Models.Customers GetCustomerById(string Id)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                return db.Customers.Where(m => m.Status == true && m.MicrosoftId == Id).SingleOrDefault();
            }
            catch
            {
                return new Models.Customers();
            }
        }

        ////Save discount margin against customer
        //public Models.Customers SaveMarginByCustomerId(float defaultPercentage, string CustomerId)
        //{
        //    try
        //    {
        //        var db = new ViewModel.Context.ConnectionStringsContext();
        //        if (db.Customers.Any(m => m.MicrosoftId == CustomerId && m.Status == true && m.MarginPercentage == defaultPercentage))
        //        {
        //            return db.Customers.Where(m => m.MicrosoftId == CustomerId && m.Status == true && m.MarginPercentage == defaultPercentage).SingleOrDefault();
        //        }
        //        else
        //        {
        //            var customer = db.Customers.Where(m => m.MicrosoftId == CustomerId && m.Status == true).SingleOrDefault();
        //            customer.MarginPercentage = defaultPercentage;
        //            db.SaveChanges();
        //            return customer;
        //        }
        //    }
        //    catch
        //    {
        //        return new Models.Customers();
        //    }
        //}
    }
}