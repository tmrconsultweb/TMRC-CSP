using Microsoft.Store.PartnerCenter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TMRC_CSP.Models;
using TMRC_CSP.ViewModel;
using TMRC_CSP.ViewModel.PartnerRepository;
using TMRC_CSP.ViewModel.Common;

namespace TMRC_CSP.Controllers
{
    [ViewModel.IsAuthenticated]
    public class AdminController : Controller
    {
        // GET: Admin/Index
        // Dashboard
        public ActionResult Index()
        {
            ViewBag.IsAuthenticated = Request.IsAuthenticated ? "true" : "false";

            ViewBag.UserName = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst("name").Value ?? "Unknown";
            ViewBag.Email = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst(ClaimTypes.Name)?.Value ??
                ((ClaimsIdentity)HttpContext.User.Identity).FindFirst(ClaimTypes.Email)?.Value;
            ViewBag.UserId = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst("UserId");

            //ViewModel.SendEmail.SendEmailRegister("ahsankamalkhan5@gmail.com", "dummy message", "AOA Ahsan kamal");
            return View();
        }

        #region Customers
        //GET:  Admin/Customers
        //Customers CRUD
        public ActionResult Customers()
        {
            //ViewModel.PartnerRepository.Customers cus = new ViewModel.PartnerRepository.Customers(ApplicationDomain.Instance);
            //SeekBasedResourceCollection<Microsoft.Store.PartnerCenter.Models.Customers.Customer> obj = cus.GetCustomerList();
            //    return View(obj);

            //ViewModel.ResellerRequest.NewRequest _VresellerRequest = new ViewModel.ResellerRequest.NewRequest();
            //return View(_VresellerRequest.GetAll());
            return View("Customers");
        }

        //Get Customers
        public JsonResult GetCustomers()
        {
            //Models.CustomerAgentRepo customerAgentRepo = new CustomerAgentRepo();

            ////Here is the logic for get customers
            //ViewModel.PartnerRepository.Customers cus = new ViewModel.PartnerRepository.Customers(ApplicationDomain.Instance);
            ////IEnumerable<Microsoft.Store.PartnerCenter.Models.Customers.Customer> obj = cus.GetCustomerList();
            //customerAgentRepo.customer = cus.GetCustomerList();

            //ViewModel.Resellers.Resellers resellers = new ViewModel.Resellers.Resellers();
            //customerAgentRepo.reseller = resellers.GetActive();

            ViewModel.PartnerRepository.Customers cus = new ViewModel.PartnerRepository.Customers(ApplicationDomain.Instance);
            IEnumerable<Models.Customers> obj = cus.GetCustomerList();

            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SelectCustomers()
        {
            return View();
        }

        public JsonResult Customer(string Id)
        {
            Session["SelectedCustomerId"] = Id;
            return Json("", JsonRequestBehavior.AllowGet);
        }


        // Delete:  Admin/DeleteRequest
        // Delete the New reseller request
        public ActionResult DeleteRequest(int Id)
        {
            ViewModel.ResellerRequest.NewRequest _VresellerRequest = new ViewModel.ResellerRequest.NewRequest();
            ViewBag.IsDeleted = _VresellerRequest.DeleteRequest(Id);
            return View("ResellerRequest", _VresellerRequest.GetAll());
        }

        // Accept: Admin/CreateCustomerAccount
        // Accept / Create a microsoft account for the request
        [HttpGet]
        public ActionResult CreateCustomerAccount(int? Id)
        {
            ViewModel.Countries.Countries country = new ViewModel.Countries.Countries();
            ViewBag.Countries = country.GetCountries();
            if (Id != null)
            {
                ViewModel.ResellerRequest.NewRequest _VresellerRequest = new ViewModel.ResellerRequest.NewRequest();
                return View(_VresellerRequest.GetReseller(Id));
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomerAccount(Models.Customers _resellerRequest)
        {
            //_resellerRequest.Id = 0;
            if (ModelState.IsValid)
            {
                //Session["_resellerRequest"] = _resellerRequest;
                //saved the new customer/Reseller
                ViewModel.ResellerRequest.NewRequest res = new ViewModel.ResellerRequest.NewRequest();
                string MicrosoftCustomerId = await res.AcceptRequest(_resellerRequest);
                if (MicrosoftCustomerId == "Unknown error occur" || MicrosoftCustomerId == "Domain name already exist")
                    ViewBag.msg = MicrosoftCustomerId;
                else
                    return RedirectToAction("Customers");
            }
            ViewModel.Countries.Countries country = new ViewModel.Countries.Countries();
            ViewBag.Countries = country.GetCountries();
            return View();
        }


        public JsonResult Subscriptions(string CustomerId)
        {
            ViewModel.Subscription.Subscriptions subscriptions = new ViewModel.Subscription.Subscriptions(ApplicationDomain.Instance);
            return Json(subscriptions.GetSubscriptions(CustomerId), JsonRequestBehavior.AllowGet);
            //return View();
        }

        public ActionResult AddSubscriptions(string Customerid)
        {
            Session["SelectedCustomerId"] = Customerid;
            return RedirectToAction("PickMicrosoftOffers");
        }

        public JsonResult EditSubscriptions(string CustomerId, string SubscriptionId)
        {
            ViewModel.Subscription.Subscriptions subscriptions = new ViewModel.Subscription.Subscriptions(ApplicationDomain.Instance);
            return Json(subscriptions.EditSubscriptions(CustomerId, SubscriptionId), JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public async Task<ActionResult> Offers(Models.Customers _resellerRequest)
        //{
        //    if (ModelState.IsValid)
        //    {


        //        //var context = new ScenarioContext();
        //        Session["_resellerRequest"] = _resellerRequest;
        //        //PartnerRepository abc = new PartnerRepository(context);


        //        //Models.Offers.RetrieveMicrosoftOffers obj = new Offers.RetrieveMicrosoftOffers();
        //        //ViewModel.PartnerRepository.Offers obj = new Offers();
        //        //obj.RetrieveMicrosoftOffers();

        //        //IEnumerable<MicrosoftOffer> microsoftOffers = await ApplicationDomain.Instance.OffersRepository.RetrieveMicrosoftOffersAsync().ConfigureAwait(false);
        //        //IEnumerable<PartnerOffer> partnerOffers = await ApplicationDomain.Instance.OffersRepository.RetrieveAsync().ConfigureAwait(false);

        //        //Find the Microsoft offers
        //        ViewModel.OffersSetup.OffersSetup _o = new ViewModel.OffersSetup.OffersSetup();
        //        List<Models.PartnerAndMicrosoftRepository> _pm = new List<Models.PartnerAndMicrosoftRepository>();
        //        _pm = await _o.RecieveOffersDetail();



        //        PartnerAndMicrosoftAndSubscriptionRepo _pms = new PartnerAndMicrosoftAndSubscriptionRepo();
        //        _pms.PartnerAndMicrosoftRepository = _pm;

        //        // abc.Offers(context);
        //        //List<MicrosoftOffer> microsoftOffers = abc.;
        //        //IEnumerable<Models.PartnerOffer> partnerOffers = await ApplicationDomain.Instance.OffersRepository.RetrieveAsync().ConfigureAwait(false);
        //        return View(_pms);
        //    }
        //    return View("CreateCustomerAccount");
        //}

        //[HttpPost]
        //public ActionResult SelectedSubscriptions(Models.PartnerAndMicrosoftAndSubscriptionRepo _m)
        //{
        //    List<Models.PartnerAndMicrosoftRepository> mic = new List<Models.PartnerAndMicrosoftRepository>();
        //    List<Models.Subscriptions> sub = new List<Subscriptions>();
        //    mic = _m.PartnerAndMicrosoftRepository.Where(m => m.MicrosoftOffer.IsAccept == true).ToList();
        //    sub = _m.Subscriptions.Where(m => m.Licenses > 0 && m.Licenses != null).ToList();
        //    Session["_mic"] = mic;
        //    Session["_sub"] = sub;
        //    Models.PartnerAndMicrosoftAndSubscriptionRepo _pms = new PartnerAndMicrosoftAndSubscriptionRepo();
        //    _pms.PartnerAndMicrosoftRepository = mic;
        //    _pms.Subscriptions = sub;
        //    return View("Confirmation", _pms);
        //}

        //public async Task<ActionResult> Confirmation()
        //{
        //    ViewModel.ResellerRequest.NewRequest res = new ViewModel.ResellerRequest.NewRequest();
        //    string MicrosoftCustomerId = await res.AcceptRequest();
        //    ViewModel.Subscription.Subscriptions subscriptions = new ViewModel.Subscription.Subscriptions(ApplicationDomain.Instance);
        //    List<Models.Subscriptions> _sub = (List<Models.Subscriptions>)Session["_sub"];
        //    List<Models.PartnerAndMicrosoftRepository> _mic = (List<Models.PartnerAndMicrosoftRepository>)Session["_mic"];
        //    for (int i = 0; i < (_sub).Count(); i++)
        //    //foreach (var i in (List<Models.Subscriptions>)Session["_sub"])
        //    {
        //        subscriptions.PlaceOrder(MicrosoftCustomerId, _mic[i].OffersSetup.MicrosoftOfferId, _sub[i].Licenses, _mic[i].OffersSetup.Title);
        //    }
        //    return View("Customers");
        //}

        public ActionResult TermsAndConditions()
        {
            ViewModel.TermConditions.TermConditionsResellers termConditionsResellers = new ViewModel.TermConditions.TermConditionsResellers();
            Models.TermsConditions terms = termConditionsResellers.GetTermsAndConditions();
            return View(terms);
        }


        #endregion


        //#region Existed Customers in partner center
        //public ActionResult Customers()
        //{
        //    ViewModel.PartnerRepository.Customers cus = new ViewModel.PartnerRepository.Customers(ApplicationDomain.Instance);
        //    SeekBasedResourceCollection<Microsoft.Store.PartnerCenter.Models.Customers.Customer> obj = cus.GetCustomerList();
        //    return View(obj);
        //}
        //#endregion

        #region Show/Add/Edit Offers setup

        public async Task<ActionResult> OffersSetup()
        {
            ViewModel.OffersSetup.OffersSetup _o = new ViewModel.OffersSetup.OffersSetup();
            List<Models.PartnerAndMicrosoftRepository> _pm = new List<Models.PartnerAndMicrosoftRepository>();
            _pm = await _o.RecieveOffersDetail();
            return View(_pm);
        }

        public ActionResult PickMicrosoftOffers()
        {
            //ViewModel.MicrosoftOfferAndPriceList.MicrosoftOfferAndPriceList _mopl = new ViewModel.MicrosoftOfferAndPriceList.MicrosoftOfferAndPriceList();
            //await _mopl.GetOffers()
            if (Session["SelectedCustomerId"] == null || Session["SelectedCustomerId"].ToString() == "")
            {
                TempData["msg"] = "Please select the customer first.";
                return RedirectToAction("SelectCustomers");
            }
            return View();
        }

        public async Task<JsonResult> GetMicrosoftOffers()
        {
            ViewModel.MicrosoftOfferAndPriceList.MicrosoftOfferAndPriceList _mopl = new ViewModel.MicrosoftOfferAndPriceList.MicrosoftOfferAndPriceList();
            return Json(await _mopl.GetOffers(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMicrosoftOffersPrice(string Id)
        {
            ViewModel.MicrosoftPriceList.PriceList _mopl = new ViewModel.MicrosoftPriceList.PriceList();
            return Json(_mopl.GetPriceById(Id), JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> AddOrUpdate(string Offerid)
        {
            MicrosoftOffer microsoftOffers = await ApplicationDomain.Instance.OffersRepository.RetrieveMicrosoftOfferByIdAsync(Offerid).ConfigureAwait(false);
            OffersSetup offersSetup = new Models.OffersSetup();

            ViewModel.OffersSetup.OffersSetup _o = new ViewModel.OffersSetup.OffersSetup();
            offersSetup = _o.GetOfferByMicrosoftOfferId(Offerid);

            if (offersSetup.MicrosoftOfferId == null)
            {
                offersSetup.MicrosoftOfferId = Offerid;
                offersSetup.Title = microsoftOffers.Offer.Name;
            }

            Models.OffersSetupRepository rep = new OffersSetupRepository();
            rep.MicrosoftOffer = microsoftOffers;
            rep.OffersSetup = offersSetup;

            return View(rep);
        }

        [HttpPost]
        public ActionResult AddOrUpdate(OffersSetup offersSetup)
        {
            ViewModel.OffersSetup.OffersSetup _o = new ViewModel.OffersSetup.OffersSetup();
            _o.AddOffersSetup(offersSetup);
            return RedirectToAction("OffersSetup");
        }


        public ActionResult DeleteOffers(string _p)
        {
            if (!string.IsNullOrEmpty(_p))
            {
                ViewModel.OffersSetup.OffersSetup OffersSetup = new ViewModel.OffersSetup.OffersSetup();
                OffersSetup.DeleteOffer(_p);
            }
            return RedirectToAction("OffersSetup");
        }

        //public async Task<Models.PartnerAndMicrosoftRepository> EditOffers(string _p)
        //{
        //    if (!string.IsNullOrEmpty(_p))
        //    {
        //        ViewModel.OffersSetup.OffersSetup _o = new ViewModel.OffersSetup.OffersSetup();
        //        List<Models.PartnerAndMicrosoftRepository> _pm = new List<Models.PartnerAndMicrosoftRepository>();
        //        _pm = await _o.RecieveOffersDetail();
        //        return View("AddOrUpdate", _pm);
        //    }
        //}
        #endregion

        #region Microsoft Offers Price List
        public ActionResult MicrosoftPriceList()
        {
            //ViewModel.MicrosoftPriceList.PriceList pl = new ViewModel.MicrosoftPriceList.PriceList();
            //List<Models.ExcelPriceList> _l = pl.GetPriceList();
            return View();
            //pl.GetPriceList();
            //return View();
        }

        public JsonResult GetPriceList()
        {
            TMRC_CSP.ViewModel.MicrosoftPriceList.PriceList priceList = new TMRC_CSP.ViewModel.MicrosoftPriceList.PriceList();
            return Json(priceList.GetGridPriceListForAdmin(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePrice()
        {
            var PriceLists = this.DeserializeObject<IEnumerable<TMRC_CSP.Models.GridPriceList>>("models");

            if (PriceLists != null)
            {
                foreach (var PriceList in PriceLists)
                {
                    Areas.Reseller.ViewModel.CustomerPrice.CustomerPrice cp = new Areas.Reseller.ViewModel.CustomerPrice.CustomerPrice();
                    cp.UpdateCustomerAndResellerPrice(PriceList);
                }
            }
            return Json(PriceLists, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportPriceList()
        {
            ViewBag.MicrosoftPriceList = "";
            return View();
        }

        [HttpPost]
        public ActionResult ImportPriceList(FormCollection formCollection)
        {
            if (Request != null)
            {
                DataTable dt = new DataTable();
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["XlsFilePath"] + fileName);
                    file.SaveAs(path);
                    if (!System.IO.Directory.Exists(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["XlsFilePath"])))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["XlsFilePath"]));
                    }
                    var excelData = new ViewModel.MicrosoftPriceList.ImportPriceList(path);
                    var sData = excelData.getData("USD");
                    List<ExcelPriceList> list = new List<ExcelPriceList>();
                    dt = sData.CopyToDataTable();
                    ViewModel.MicrosoftPriceList.PriceList priceList = new ViewModel.MicrosoftPriceList.PriceList();
                    list = excelData.ImportPrice(dt);
                    ViewBag.MicrosoftPriceList = list;
                    TempData["MicrosoftPriceList"] = list;
                }
            }
            return View("ImportPriceList");
        }


        public ActionResult SaveMicrosoftPrice()
        {
            List<ExcelPriceList> _m = TempData["MicrosoftPriceList"] as List<ExcelPriceList>;
            ViewModel.MicrosoftPriceList.PriceList pl = new ViewModel.MicrosoftPriceList.PriceList();
            pl.SavePriceList(_m);

            return View("MicrosoftPriceList", _m);
        }

        public FileResult DownloadPriceList()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Files/PriceList.xlsx"));
            string fileName = "PriceList.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        #endregion


        #region Terms & conditions for Users
        public ActionResult TermConditionsUsers()
        {
            ViewModel.TermConditions.TermConditionsUsers termConditionsUsers = new ViewModel.TermConditions.TermConditionsUsers();
            return View(termConditionsUsers.GetTermsAndConditions());
        }

        [HttpPost]
        public ActionResult TermConditionsUsers(Models.TermsConditions tc)
        {
            if (ModelState.IsValid)
            {
                ViewModel.TermConditions.TermConditionsUsers termConditionsUsers = new ViewModel.TermConditions.TermConditionsUsers();
                termConditionsUsers.CheckIfExist(tc);
            }
            return View();
        }
        #endregion

        #region Terms & conditions for Resellers
        public ActionResult TermConditionsResellers()
        {
            ViewModel.TermConditions.TermConditionsResellers termConditionsResellers = new ViewModel.TermConditions.TermConditionsResellers();
            return View(termConditionsResellers.GetTermsAndConditions());
        }

        [HttpPost]
        public ActionResult TermConditionsResellers(Models.TermsConditions tc)
        {
            if (ModelState.IsValid)
            {
                ViewModel.TermConditions.TermConditionsResellers termConditionsResellers = new ViewModel.TermConditions.TermConditionsResellers();
                termConditionsResellers.CheckIfExist(tc);
            }
            return View();
        }
        #endregion

        #region Default Percentage for Users
        public ActionResult DefaultMarginUsers()
        {
            ViewModel.DefaultMargin.DefaultMarginUsers defaultMarginUsers = new ViewModel.DefaultMargin.DefaultMarginUsers();
            return View(defaultMarginUsers.GetDefaultMargin());
        }

        [HttpPost]
        public ActionResult DefaultMarginUsers(Models.DefaultMargin d)
        {
            if (ModelState.IsValid)
            {
                ViewModel.DefaultMargin.DefaultMarginUsers defaultMarginUsers = new ViewModel.DefaultMargin.DefaultMarginUsers();
                defaultMarginUsers.CheckIfExist(d);
            }
            return View();
        }
        #endregion

        #region Default Percentage for Resellers
        public ActionResult DefaultMarginResellers()
        {
            ViewModel.DefaultMargin.DefaultMarginResellers defaultMarginResellers = new ViewModel.DefaultMargin.DefaultMarginResellers();
            return View(defaultMarginResellers.GetDefaultMargin());
        }

        [HttpPost]
        public ActionResult DefaultMarginResellers(Models.DefaultMargin d)
        {
            if (ModelState.IsValid)
            {
                ViewModel.DefaultMargin.DefaultMarginResellers defaultMarginResellers = new ViewModel.DefaultMargin.DefaultMarginResellers();
                defaultMarginResellers.CheckIfExist(d);
            }
            return View();
        }


        #endregion


        #region Promo Code

        public ActionResult PromoCode()
        {
            ViewModel.PromotionCodes.PromotionCodes promotionCodes = new ViewModel.PromotionCodes.PromotionCodes();
            return View(promotionCodes.GetList());
        }

        public ActionResult GenerateCode(int? id)
        {
            Models.PromotionCodes promotionCodes = new PromotionCodes();
            if (id == null)
            {
                Random generator = new Random();
                string promoCode = generator.Next(0, 999999).ToString("D6");
                TempData["PromoCode"] = promoCode;
                promotionCodes.Code = promoCode;
                promotionCodes.ExpiryDate = DateTime.Now;
            }
            else
            {
                ViewModel.PromotionCodes.PromotionCodes promo = new ViewModel.PromotionCodes.PromotionCodes();
                promotionCodes = promo.GetPromoCodeById(id);
                promotionCodes.ExpiryDate = promotionCodes.ExpiryDate.Date;
            }


            return View(promotionCodes);
        }

        [HttpPost]
        public ActionResult GenerateCode(Models.PromotionCodes p)
        {
            if (p.Code == null || p.Code == "")
                p.Code = (string)TempData["PromoCode"];
            ViewModel.PromotionCodes.PromotionCodes promotionCodes = new ViewModel.PromotionCodes.PromotionCodes();
            promotionCodes.SavePromoCode(p);
            return RedirectToAction("PromoCode");
        }

        //public ActionResult EditPromoCode(int id, string code,DateTime exp)
        //{

        //}

        public ActionResult DeletePromoCode(int id)
        {
            ViewModel.PromotionCodes.PromotionCodes promotionCodes = new ViewModel.PromotionCodes.PromotionCodes();
            promotionCodes.DeletePromoCode(id);
            return RedirectToAction("PromoCode");
        }
        #endregion

        #region Resellers/Agents

        public ActionResult Resellers()
        {
            ViewModel.Resellers.Resellers resellers = new ViewModel.Resellers.Resellers();
            return View(resellers.Getlist());
        }

        [HttpGet]
        public ActionResult CreateReseller(int? id)
        {
            ViewModel.DefaultMargin.DefaultMarginResellers defaultMarginResellers = new ViewModel.DefaultMargin.DefaultMarginResellers();

            Models.Reseller res = new Reseller();
            res.Margin = defaultMarginResellers.GetDefaultMargin().DefaultPercentage;
            if (id != null)
            {
                ViewModel.Resellers.Resellers resellers = new ViewModel.Resellers.Resellers();
                res = resellers.GetById(id);
            }
            ViewModel.Countries.Countries country = new ViewModel.Countries.Countries();
            ViewBag.Countries = country.GetCountries();
            return View("AddReseller", res);
        }

        [HttpPost]
        public ActionResult CreateReseller(Models.Reseller r)
        {
            if (ModelState.IsValid)
            {
                ViewModel.Resellers.Resellers resellers = new ViewModel.Resellers.Resellers();
                resellers.Save(r);
                return RedirectToAction("Resellers");
            }
            ViewModel.Countries.Countries country = new ViewModel.Countries.Countries();
            ViewBag.Countries = country.GetCountries();
            return View("AddReseller");
        }


        public JsonResult DeleteReseller(int id, bool IsDelete)
        {
            ViewModel.Resellers.Resellers resellers = new ViewModel.Resellers.Resellers();
            return Json(resellers.Delete(id, IsDelete), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Users

        //public ActionResult Users(string Id)
        //{
        //    ViewModel.Users.Users users = new ViewModel.Users.Users(ApplicationDomain.UserInstance);
        //    //IEnumerable<Microsoft.Store.PartnerCenter.Models.Customers.Customer> obj =
        //    users.Get(Id);
        //    return View();
        //}



        #endregion

        #region Cart
        public JsonResult AddToCart(string Id, int License)
        {
            ViewModel.AddToCart.AddToCart cart = new ViewModel.AddToCart.AddToCart();
            return Json(cart.Save(Id, License, 0, Session["SelectedCustomerId"].ToString()), JsonRequestBehavior.AllowGet);  // the 0 mean it's a Admin
        }

        public ActionResult Cart()
        {
            double? margin = 0;
            if (Session["SelectedCustomerId"] == null || Session["SelectedCustomerId"].ToString() == "")
            {
                TempData["msg"] = "Please select the customer first.";
                return RedirectToAction("SelectCustomers");
            }
            else
            {
                if (TempData["Margin"] == null)
                {
                    Session["PromoCode"] = null;
                    margin = GetCustomerMargin();
                    if (margin == null || margin == 0)
                    {
                        ViewModel.DefaultMargin.DefaultMarginUsers defaultMarginUsers = new ViewModel.DefaultMargin.DefaultMarginUsers();
                        margin = defaultMarginUsers.GetDefaultMargin().DefaultPercentage;
                    }
                    if (margin == null)
                        margin = 0;
                    TempData["Margin"] = margin;
                    Session["Margin"] = margin;
                }
                ViewModel.AddToCart.AddToCart cart = new ViewModel.AddToCart.AddToCart();
                return View(cart.Get(0, Session["SelectedCustomerId"].ToString(), true));
            }
        }

        [HttpPost]
        public ActionResult Cart(string PromoCode, string DefaultPercentage)
        {
            if (PromoCode != null && PromoCode != "")
            {
                Session["PromoCode"] = PromoCode;
                ViewModel.PromotionCodes.PromotionCodes promotionCodes = new ViewModel.PromotionCodes.PromotionCodes();
                var code = promotionCodes.GetPromoCodeByCoode(Session["PromoCode"].ToString());
                if (code != null)
                {
                    Session["Margin"] = code.Amount;
                    TempData["Margin"] = code.Amount;
                }
            }
            else
            {
                Session["Margin"] = DefaultPercentage;
                TempData["Margin"] = DefaultPercentage;
            }
            return RedirectToAction("cart");
        }

        public JsonResult DeleteItem(int Id)
        {
            ViewModel.AddToCart.AddToCart cart = new ViewModel.AddToCart.AddToCart();
            return Json(cart.Delete(Id, 0), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAll()
        {
            ViewModel.AddToCart.AddToCart cart = new ViewModel.AddToCart.AddToCart();
            cart.DeleteAll(0);
            return RedirectToAction("PickMicrosoftOffers");
        }

        public JsonResult UpdateItem(int Id, int License, string BillingFrequency)
        {
            ViewModel.AddToCart.AddToCart addToCart = new ViewModel.AddToCart.AddToCart();
            return Json(addToCart.UpdateById(Id, License, BillingFrequency), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Confirmation()
        {
            if (Session["SelectedCustomerId"] == null || Session["SelectedCustomerId"].ToString() == "")
            {
                TempData["msg"] = "Please select the customer first.";
                return RedirectToAction("SelectCustomers");
            }
            ViewModel.Invoice.Invoice invoice = new ViewModel.Invoice.Invoice();
            Models.Invoice inv = invoice.Get(Session["SelectedCustomerId"].ToString());

            return View(inv);
        }

        public ActionResult BuyNow()
        {
            if (Session["SelectedCustomerId"] == null || Session["SelectedCustomerId"].ToString() == "")
            {
                TempData["msg"] = "Please select the customer first.";
                return RedirectToAction("SelectCustomers");
            }
            ViewModel.BuyNow.BuyNow buyNow = new ViewModel.BuyNow.BuyNow();
            ViewBag.msg = buyNow.Buy(0, Session["SelectedCustomerId"].ToString(), Convert.ToInt64(Session["InvoiceNo"]), Convert.ToDouble(Session["Margin"]));
            return View();
        }

        #endregion

        #region Promo Code
        public ActionResult AddPromoCode()
        {
            double? margin = 0;
            if (Session["SelectedCustomerId"] == null || Session["SelectedCustomerId"].ToString() == "")
            {
                TempData["msg"] = "Please select the customer first.";
                return RedirectToAction("SelectCustomers");
            }
            else
            {
                GetCustomerMargin();

                if (margin == null || margin == 0)
                {
                    ViewModel.DefaultMargin.DefaultMarginUsers defaultMarginUsers = new ViewModel.DefaultMargin.DefaultMarginUsers();
                    margin = defaultMarginUsers.GetDefaultMargin().DefaultPercentage;
                }

                if (margin == null)
                    margin = 0;

            }
            return View(margin);
        }

        private double? GetCustomerMargin()
        {
            try
            {
                if (Session["PromoCode"] != null && Session["PromoCode"].ToString() != "")
                {
                    ViewModel.PromotionCodes.PromotionCodes promotionCodes = new ViewModel.PromotionCodes.PromotionCodes();
                    return promotionCodes.GetPromoCodeByCoode(Session["PromoCode"].ToString()).Amount;
                }
                return 0;

            }
            catch
            {
                return 0;
            }

        }


        #endregion


        #region Reseller Customers
        public JsonResult GetResellerCustomers(int Id)
        {
            ViewModel.ResellerCustomers.ResellerCustomers resellerCustomers = new ViewModel.ResellerCustomers.ResellerCustomers();
            return Json(resellerCustomers.Get(Id), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Reseller Price
        public JsonResult GetResellerPrice(int Id)
        {
            ViewModel.ResellerPrice.ResellerPrice resellerPrice = new ViewModel.ResellerPrice.ResellerPrice();
            return Json(resellerPrice.Get(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateResellerPrice(int Id)
        {
            var PriceLists = this.DeserializeObject<IEnumerable<TMRC_CSP.Models.GridPriceList>>("models");

            if (PriceLists != null)
            {
                foreach (var PriceList in PriceLists)
                {
                    ViewModel.ResellerPrice.ResellerPrice resellerPrice = new ViewModel.ResellerPrice.ResellerPrice();
                    resellerPrice.UpdateResellerPrice(Id, PriceList);
                }
            }
            return Json(PriceLists, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Reset(int Id)
        {
            ViewModel.ResellerPrice.ResellerPrice resellerPrice = new ViewModel.ResellerPrice.ResellerPrice();
            return Json(resellerPrice.Reset(Id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Manage Sale tax

        public ActionResult SaleTax()
        {
            ViewModel.Countries.Countries country = new ViewModel.Countries.Countries();
            ViewBag.Countries = country.GetCountries();
            Models.SaleTax saleTax = new Models.SaleTax
            {
                Id = 0
            };
            return View(saleTax);
        }

        [HttpPost]
        public ActionResult SaleTax(Models.SaleTax _s)
        {
            ViewModel.Countries.Countries country = new ViewModel.Countries.Countries();
            ViewBag.Countries = country.GetCountries();
            if (ModelState.IsValid)
            {
                ViewModel.SaleTax.SaleTax saleTax = new ViewModel.SaleTax.SaleTax();
                ViewBag.msg = saleTax.Save(_s, 0); //0 means it's Admin
            }
            return View();
        }

        public JsonResult GetSaleTax()
        {
            ViewModel.SaleTax.SaleTax saleTax = new ViewModel.SaleTax.SaleTax();
            return Json(saleTax.Get(0), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Company Setting

        public ActionResult Company()
        {
            ViewModel.Company.Company company = new ViewModel.Company.Company();
            return View(company.Get(0));
        }

        [HttpPost]
        public ActionResult Company(Models.Company _c)
        {
            ViewModel.Company.Company company = new ViewModel.Company.Company();
            if (ModelState.IsValid)
                ViewBag.msg = company.Save(_c, 0);
            return View(company.Get(0));
        }

        #endregion


        #region Invoice setting

        public ActionResult InvoiceSetting()
        {
            TMRC_CSP.ViewModel.Invoice.InvoiceSetting invoice = new TMRC_CSP.ViewModel.Invoice.InvoiceSetting();
            return View(invoice.Get(0));
        }

        [HttpPost]
        public ActionResult InvoiceSetting(TMRC_CSP.Models.InvoiceSetting _i)
        {
            TMRC_CSP.ViewModel.Invoice.InvoiceSetting invoice = new TMRC_CSP.ViewModel.Invoice.InvoiceSetting();
            if (ModelState.IsValid)
                ViewBag.msg = invoice.Save(_i, 0);
            return View(invoice.Get(0));
        }

        #endregion 
    }
}