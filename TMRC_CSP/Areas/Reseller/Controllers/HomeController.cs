using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TMRC_CSP.ViewModel.Common;
using TMRC_CSP.ViewModel.PartnerRepository;

namespace TMRC_CSP.Areas.Reseller.Controllers
{
    [ViewModel.Account.Authenticated]
    public class HomeController : Controller
    {
        // GET: Reseller/Home (Dashboard)
        public ActionResult Index()
        {
            return View();
        }


        #region Change Password
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(Models.ChangePassword c)
        {
            if (ModelState.IsValid)
            {
                ViewModel.Account.ChangePassword cp = new ViewModel.Account.ChangePassword();
                ViewBag.msg = cp.SavePassword(c);
            }
            return View();
        }

        #endregion

        #region Profile

        public ActionResult Profile()
        {
            return View(ViewModel.Account.Login._r);
        }

        public ActionResult EditProfile(int id)
        {
            return View(ViewModel.Account.Login._r);
        }

        [HttpPost]
        public ActionResult EditProfile(TMRC_CSP.Models.Reseller r)
        {
            ViewModel.Profile.Profile profile = new ViewModel.Profile.Profile();
            ViewBag.msg = profile.Save(r);
            return View(ViewModel.Account.Login._r);
        }

        #endregion

        #region Terms & Conditions
        public ActionResult TermsAndConditions()
        {
            ViewModel.TermConditions.TermAndConditions termConditions = new ViewModel.TermConditions.TermAndConditions();
            return View(termConditions.GetTermsAndConditions());
        }

        [HttpPost]
        public ActionResult TermsAndConditions(TMRC_CSP.Models.TermsConditions tc)
        {
            if (ModelState.IsValid)
            {
                ViewModel.TermConditions.TermAndConditions termAndConditions = new ViewModel.TermConditions.TermAndConditions();
                termAndConditions.CheckIfExist(tc, ViewModel.Account.Login._r.Id);
            }
            return View();
        }
        #endregion

        #region Default Margin For User
        public ActionResult DefaultMarginUser()
        {
            ViewModel.DefaultMargin.DefaultMarginUser dmu = new ViewModel.DefaultMargin.DefaultMarginUser();
            return View(dmu.GetDefaultMargin());
        }

        [HttpPost]
        public ActionResult DefaultMarginUser(TMRC_CSP.Models.DefaultMargin dm)
        {
            if (ModelState.IsValid)
            {
                ViewModel.DefaultMargin.DefaultMarginUser dmu = new ViewModel.DefaultMargin.DefaultMarginUser();
                dmu.CheckIfExist(dm, ViewModel.Account.Login._r.Id);
            }
            return View();
        }
        #endregion

        #region CSP Offers
        public ActionResult OffersPriceList()
        {
            return View();
        }


        public JsonResult GetPriceList()
        {
            TMRC_CSP.ViewModel.ResellerPrice.ResellerPrice priceList = new TMRC_CSP.ViewModel.ResellerPrice.ResellerPrice();
            return Json(priceList.GetForReseller(ViewModel.Account.Login._r.Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePrice()
        {
            var PriceLists = this.DeserializeObject<IEnumerable<TMRC_CSP.Models.GridPriceList>>("models");

            if (PriceLists != null)
            {
                foreach (var PriceList in PriceLists)
                {
                    ViewModel.CustomerPrice.CustomerPrice cp = new ViewModel.CustomerPrice.CustomerPrice();
                    cp.Save(ViewModel.Account.Login._r.Id, PriceList);
                }
            }
            return Json(PriceLists, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PickMicrosoftOffers()
        {
            if (Session["SelectedCustomerId"] == null || Session["SelectedCustomerId"].ToString() == "")
            {
                TempData["msg"] = "Please select the customer first.";
                return RedirectToAction("SelectCustomers", "Home", new { area = "Reseller" });
            }
            return View();
        }

        public async Task<JsonResult> GetMicrosoftOffers()
        {
            TMRC_CSP.ViewModel.MicrosoftOfferAndPriceList.MicrosoftOfferAndPriceList _mopl = new TMRC_CSP.ViewModel.MicrosoftOfferAndPriceList.MicrosoftOfferAndPriceList();
            return Json(await _mopl.GetOffers(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMicrosoftOffersPrice(string Id)
        {
            TMRC_CSP.ViewModel.ResellerPrice.ResellerPrice resellerPrice = new TMRC_CSP.ViewModel.ResellerPrice.ResellerPrice();
            return Json(resellerPrice.GetPriceList(ViewModel.Account.Login._r.Id, Id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Customers

        public ActionResult Customers()
        {
            return View();
        }

        public ActionResult SelectCustomers()
        {
            return View();
        }

        public JsonResult GetCustomers()
        {
            TMRC_CSP.ViewModel.ResellerCustomers.ResellerCustomers resellerCustomers = new TMRC_CSP.ViewModel.ResellerCustomers.ResellerCustomers();
            return Json(resellerCustomers.Get(ViewModel.Account.Login._r.Id), JsonRequestBehavior.AllowGet);
            //TMRC_CSP.ViewModel.PartnerRepository.Customers cus = new TMRC_CSP.ViewModel.PartnerRepository.Customers(ApplicationDomain.Instance);
            //IEnumerable<TMRC_CSP.Models.Customers> obj = cus.GetCustomerList();
            //return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddCustomer(int? Id)
        {
            TMRC_CSP.ViewModel.Countries.Countries country = new TMRC_CSP.ViewModel.Countries.Countries();
            ViewBag.Countries = country.GetCountries();
            if (Id != null)
            {
                TMRC_CSP.ViewModel.ResellerRequest.NewRequest _VresellerRequest = new TMRC_CSP.ViewModel.ResellerRequest.NewRequest();
                return View(_VresellerRequest.GetReseller(Id));
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(TMRC_CSP.Models.Customers _resellerRequest)
        {
            if (ModelState.IsValid)
            {
                //Session["_resellerRequest"] = _resellerRequest;
                //saved the new customer/Reseller
                TMRC_CSP.ViewModel.ResellerRequest.NewRequest res = new TMRC_CSP.ViewModel.ResellerRequest.NewRequest();
                string MicrosoftCustomerId = await res.AcceptRequest(_resellerRequest, ViewModel.Account.Login._r.Id);
                if (MicrosoftCustomerId == "Unknown error occur" || MicrosoftCustomerId == "Domain name already exist")
                    ViewBag.msg = MicrosoftCustomerId;
                else
                    return RedirectToAction("Customers");
            }
            TMRC_CSP.ViewModel.Countries.Countries country = new TMRC_CSP.ViewModel.Countries.Countries();
            ViewBag.Countries = country.GetCountries();
            return View();
        }

        public JsonResult Customer(string Id)
        {
            Session["SelectedCustomerId"] = Id;
            return Json("", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Subscriptions

        public JsonResult Subscriptions(string CustomerId)
        {
            TMRC_CSP.ViewModel.Subscription.Subscriptions subscriptions = new TMRC_CSP.ViewModel.Subscription.Subscriptions(ApplicationDomain.Instance);
            return Json(subscriptions.GetSubscriptions(CustomerId), JsonRequestBehavior.AllowGet);
            //return View();
        }

        public ActionResult AddSubscriptions(string Customerid)
        {
            Session["SelectedCustomerId"] = Customerid;
            return RedirectToAction("PickMicrosoftOffers");
        }

        #endregion

        #region Cart
        public JsonResult AddToCart(string Id, int License)
        {
            TMRC_CSP.ViewModel.AddToCart.AddToCart cart = new TMRC_CSP.ViewModel.AddToCart.AddToCart();
            return Json(cart.Save(Id, License, ViewModel.Account.Login._r.Id, Session["SelectedCustomerId"].ToString()), JsonRequestBehavior.AllowGet);  // the 0 mean it's a Admin
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
                    margin = GetCustomerMargin();
                    if (margin == null || margin == 0)
                    {
                        ViewModel.DefaultMargin.DefaultMarginUser defaultMarginUsers = new ViewModel.DefaultMargin.DefaultMarginUser();
                        margin = defaultMarginUsers.GetDefaultMargin().DefaultPercentage;
                    }
                    if (margin == null)
                        margin = 0;
                    TempData["Margin"] = margin;
                    Session["Margin"] = margin;
                }
                TMRC_CSP.ViewModel.AddToCart.AddToCart cart = new TMRC_CSP.ViewModel.AddToCart.AddToCart();
                return View(cart.Get(ViewModel.Account.Login._r.Id, Session["SelectedCustomerId"].ToString(), true));
            }

        }

        [HttpPost]
        public ActionResult Cart(string PromoCode, string DefaultPercentage)
        {
            if (PromoCode != null && PromoCode != "")
            {
                Session["PromoCode"] = PromoCode;
                TMRC_CSP.ViewModel.PromotionCodes.PromotionCodes promotionCodes = new TMRC_CSP.ViewModel.PromotionCodes.PromotionCodes();
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
            TMRC_CSP.ViewModel.AddToCart.AddToCart cart = new TMRC_CSP.ViewModel.AddToCart.AddToCart();
            return Json(cart.Delete(Id, ViewModel.Account.Login._r.Id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAll()
        {
            TMRC_CSP.ViewModel.AddToCart.AddToCart cart = new TMRC_CSP.ViewModel.AddToCart.AddToCart();
            cart.DeleteAll(ViewModel.Account.Login._r.Id);
            return RedirectToAction("PickMicrosoftOffers");
        }

        public JsonResult UpdateItem(int Id, int License, string BillingFrequency)
        {
            TMRC_CSP.ViewModel.AddToCart.AddToCart addToCart = new TMRC_CSP.ViewModel.AddToCart.AddToCart();
            return Json(addToCart.UpdateById(Id, License, BillingFrequency), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Confirmation()
        {
            if (Session["SelectedCustomerId"] == null || Session["SelectedCustomerId"].ToString() == "")
            {
                TempData["msg"] = "Please select the customer first.";
                return RedirectToAction("SelectCustomers");
            }
            TMRC_CSP.ViewModel.Invoice.Invoice invoice = new TMRC_CSP.ViewModel.Invoice.Invoice();
            TMRC_CSP.Models.Invoice inv = invoice.Get(Session["SelectedCustomerId"].ToString(), ViewModel.Account.Login._r.Id);
            return View(inv);
        }

        public ActionResult BuyNow()
        {
            if (Session["SelectedCustomerId"] == null || Session["SelectedCustomerId"].ToString() == "")
            {
                TempData["msg"] = "Please select the customer first.";
                return RedirectToAction("SelectCustomers");
            }
            TMRC_CSP.ViewModel.BuyNow.BuyNow buyNow = new TMRC_CSP.ViewModel.BuyNow.BuyNow();
            ViewBag.msg = buyNow.Buy(ViewModel.Account.Login._r.Id, Session["SelectedCustomerId"].ToString(), Convert.ToInt64(Session["InvoiceNo"]), Convert.ToDouble(Session["Margin"]));
            return View();
        }

        private double? GetCustomerMargin()
        {
            try
            {
                if (Session["PromoCode"] != null && Session["PromoCode"].ToString() != "")
                {
                    TMRC_CSP.ViewModel.PromotionCodes.PromotionCodes promotionCodes = new TMRC_CSP.ViewModel.PromotionCodes.PromotionCodes();
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

        #region Manage Sale tax

        public ActionResult SaleTax()
        {
            TMRC_CSP.ViewModel.SaleTax.SaleTax saleTax = new TMRC_CSP.ViewModel.SaleTax.SaleTax();
            return View(saleTax.Get(ViewModel.Account.Login._r.Id));
        }

        [HttpPost]
        public ActionResult SaleTax(TMRC_CSP.Models.SaleTax _s)
        {
            if (ModelState.IsValid)
            {
                TMRC_CSP.ViewModel.SaleTax.SaleTax saleTax = new TMRC_CSP.ViewModel.SaleTax.SaleTax();
                ViewBag.msg = saleTax.Save(_s, ViewModel.Account.Login._r.Id);
            }
            return View();
        }
        #endregion

        #region Company Setting

        public ActionResult Company()
        {
            TMRC_CSP.ViewModel.Company.Company company = new TMRC_CSP.ViewModel.Company.Company();
            return View(company.Get(ViewModel.Account.Login._r.Id));
        }

        [HttpPost]
        public ActionResult Company(TMRC_CSP.Models.Company _c)
        {
            TMRC_CSP.ViewModel.Company.Company company = new TMRC_CSP.ViewModel.Company.Company();
            if (ModelState.IsValid)
                ViewBag.msg = company.Save(_c, ViewModel.Account.Login._r.Id);
            return View(company.Get(ViewModel.Account.Login._r.Id));
        }

        #endregion

        #region Invoice setting

        public ActionResult InvoiceSetting()
        {
            TMRC_CSP.ViewModel.Invoice.InvoiceSetting invoice = new TMRC_CSP.ViewModel.Invoice.InvoiceSetting();
            return View(invoice.Get(ViewModel.Account.Login._r.Id));
        }

        [HttpPost]
        public ActionResult InvoiceSetting(TMRC_CSP.Models.InvoiceSetting _i)
        {
            TMRC_CSP.ViewModel.Invoice.InvoiceSetting invoice = new TMRC_CSP.ViewModel.Invoice.InvoiceSetting();
            if (ModelState.IsValid)
                ViewBag.msg = invoice.Save(_i, ViewModel.Account.Login._r.Id);
            return View(invoice.Get(ViewModel.Account.Login._r.Id));
        }

        #endregion
    }


}