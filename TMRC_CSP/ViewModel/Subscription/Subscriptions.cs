using Microsoft.Store.PartnerCenter.Models.Orders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TMRC_CSP.Models;
using TMRC_CSP.ViewModel.PartnerRepository;

namespace TMRC_CSP.ViewModel.Subscription
{
    public class Subscriptions : DomainObject
    {
        ///// <summary>
        ///// Gets the summary of subscriptions for a portal customer. 
        ///// </summary>
        ///// <param name="customerId">The customer Id.</param>
        ///// <returns>Subscription Summary.</returns>
        //private async Task<SubscriptionsSummary> GetSubscriptionSummaryAsync(string customerId)
        //{
        //    DateTime startTime = DateTime.Now;
        //    IEnumerable<CustomerSubscriptionEntity> customerSubscriptions = await ApplicationDomain.Instance.CustomerSubscriptionsRepository.RetrieveAsync(customerId).ConfigureAwait(false);
        //    IEnumerable<CustomerPurchaseEntity> customerSubscriptionsHistory = await ApplicationDomain.Instance.CustomerPurchasesRepository.RetrieveAsync(customerId).ConfigureAwait(false);
        //    IEnumerable<PartnerOffer> allPartnerOffers = await ApplicationDomain.Instance.OffersRepository.RetrieveAsync().ConfigureAwait(false);
        //    IEnumerable<MicrosoftOffer> currentMicrosoftOffers = await ApplicationDomain.Instance.OffersRepository.RetrieveMicrosoftOffersAsync().ConfigureAwait(false);

        //    // start building the summary.                 
        //    decimal summaryTotal = 0;

        //    // format all responses to client using portal locale. 
        //    CultureInfo responseCulture = new CultureInfo(ApplicationDomain.Instance.PortalLocalization.Locale);
        //    List<SubscriptionViewModel> customerSubscriptionsView = new List<SubscriptionViewModel>();

        //    // iterate through and build the list of customer's subscriptions. 
        //    foreach (CustomerSubscriptionEntity subscription in customerSubscriptions)
        //    {
        //        decimal subscriptionTotal = 0;
        //        int licenseTotal = 0;
        //        List<SubscriptionHistory> historyItems = new List<SubscriptionHistory>();

        //        // collect the list of history items for this subcription.  
        //        IOrderedEnumerable<CustomerPurchaseEntity> subscriptionHistoryList = customerSubscriptionsHistory
        //            .Where(historyItem => historyItem.SubscriptionId == subscription.SubscriptionId)
        //            .OrderBy(historyItem => historyItem.TransactionDate);

        //        // iterate through and build the SubsriptionHistory for this subscription. 
        //        foreach (CustomerPurchaseEntity historyItem in subscriptionHistoryList)
        //        {
        //            decimal orderTotal = Math.Round(historyItem.SeatPrice * historyItem.SeatsBought, responseCulture.NumberFormat.CurrencyDecimalDigits);
        //            historyItems.Add(new SubscriptionHistory()
        //            {
        //                OrderTotal = orderTotal.ToString("C", responseCulture),                                 // Currency format.
        //                PricePerSeat = historyItem.SeatPrice.ToString("C", responseCulture),                    // Currency format. 
        //                SeatsBought = historyItem.SeatsBought.ToString("G", responseCulture),                   // General format.  
        //                OrderDate = historyItem.TransactionDate.ToLocalTime().ToString("d", responseCulture),   // Short date format. 
        //                OperationType = GetOperationType(historyItem.PurchaseType)                         // Localized Operation type string. 
        //            });

        //            // Increment the subscription total. 
        //            licenseTotal += historyItem.SeatsBought;

        //            // Increment the subscription total. 
        //            subscriptionTotal += orderTotal;
        //        }

        //        PartnerOffer partnerOfferItem = allPartnerOffers.FirstOrDefault(offer => offer.Id == subscription.PartnerOfferId);
        //        string subscriptionTitle = partnerOfferItem.Title;
        //        string portalOfferId = partnerOfferItem.Id;
        //        decimal portalOfferPrice = partnerOfferItem.Price;

        //        DateTime subscriptionExpiryDate = subscription.ExpiryDate.ToUniversalTime();
        //        int remainingDays = (subscriptionExpiryDate.Date - DateTime.UtcNow.Date).Days;
        //        bool isRenewable = remainingDays <= 30;
        //        bool isEditable = DateTime.UtcNow.Date <= subscriptionExpiryDate.Date;

        //        // TODO :: Handle Microsoft offer being pulled back due to EOL. 

        //        // Temporarily mark this partnerOffer item as inactive and dont allow store front customer to manage this subscription. 
        //        MicrosoftOffer alignedMicrosoftOffer = currentMicrosoftOffers.FirstOrDefault(offer => offer.Offer.Id == partnerOfferItem.MicrosoftOfferId);
        //        if (alignedMicrosoftOffer == null)
        //        {
        //            partnerOfferItem.IsInactive = true;
        //        }

        //        if (partnerOfferItem.IsInactive)
        //        {
        //            // in case the offer is inactive (marked for deletion) then dont allow renewals or editing on this subscription tied to this offer. 
        //            isRenewable = false;
        //            isEditable = false;
        //        }

        //        // Compute the pro rated price per seat for this subcription & return for client side processing during updates. 
        //        decimal proratedPerSeatPrice = Math.Round(CommerceOperations.CalculateProratedSeatCharge(subscription.ExpiryDate, portalOfferPrice), responseCulture.NumberFormat.CurrencyDecimalDigits);

        //        SubscriptionViewModel subscriptionItem = new SubscriptionViewModel()
        //        {
        //            SubscriptionId = subscription.SubscriptionId,
        //            FriendlyName = subscriptionTitle,
        //            PortalOfferId = portalOfferId,
        //            PortalOfferPrice = portalOfferPrice.ToString("C", responseCulture),
        //            IsRenewable = isRenewable,                                                              // IsRenewable is true if subscription is going to expire in 30 days.                         
        //            IsEditable = isEditable,                                                                // IsEditable is true if today is lesser or equal to subscription expiry date.                                                
        //            LicensesTotal = licenseTotal.ToString("G", responseCulture),                            // General format. 
        //            SubscriptionTotal = subscriptionTotal.ToString("C", responseCulture),                   // Currency format.
        //            SubscriptionExpiryDate = subscriptionExpiryDate.Date.ToString("d", responseCulture),    // Short date format. 
        //            SubscriptionOrderHistory = historyItems,
        //            SubscriptionProRatedPrice = proratedPerSeatPrice
        //        };

        //        // add this subcription to the customer's subscription list.
        //        customerSubscriptionsView.Add(subscriptionItem);

        //        // Increment the summary total. 
        //        summaryTotal += subscriptionTotal;
        //    }

        //    // Capture the request for the customer summary for analysis.
        //    Dictionary<string, string> eventProperties = new Dictionary<string, string> { { "CustomerId", customerId } };

        //    // Track the event measurements for analysis.
        //    Dictionary<string, double> eventMetrics = new Dictionary<string, double> { { "ElapsedMilliseconds", DateTime.Now.Subtract(startTime).TotalMilliseconds }, { "NumberOfSubscriptions", customerSubscriptionsView.Count } };

        //    ApplicationDomain.Instance.TelemetryService.Provider.TrackEvent("GetSubscriptionSummaryAsync", eventProperties, eventMetrics);

        //    // Sort List of subscriptions based on portal offer name. 
        //    return new SubscriptionsSummary()
        //    {
        //        Subscriptions = customerSubscriptionsView.OrderBy(subscriptionItem => subscriptionItem.FriendlyName),
        //        SummaryTotal = summaryTotal.ToString("C", responseCulture)      // Currency format.
        //    };
        //}


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrder"/> class.
        /// </summary>
        /// <param name="context">The scenario context.</param>
        public Subscriptions(ApplicationDomain applicationDomain) : base(applicationDomain)
        {
        }

        /// <summary>
        /// Executes the scenario.
        /// </summary>
        public Microsoft.Store.PartnerCenter.Models.Orders.Order PlaceOrder(string customerId, string offerId, int quantity, Models.PurchaseUnit PurchaseUnit, string Title= "new offer purchase")
        {
            //var partnerOperations = this.Context.UserPartnerOperations;

            //bydefault billing frequency monthly set
            var order = new Order()
            {
                ReferenceCustomerId = customerId,
                BillingCycle = PurchaseUnit == PurchaseUnit.Years ? Microsoft.Store.PartnerCenter.Models.Offers.BillingCycleType.Annual : PurchaseUnit == PurchaseUnit.Months ? Microsoft.Store.PartnerCenter.Models.Offers.BillingCycleType.Monthly : Microsoft.Store.PartnerCenter.Models.Offers.BillingCycleType.None,
                LineItems = new List<OrderLineItem>()
                {
                    new OrderLineItem()
                    {
                        OfferId = offerId,
                        FriendlyName = Title,
                        Quantity = quantity,
                        
                    }
                }
            };

            return ApplicationDomain.PartnerCenterClient.Customers.ById(customerId).Orders.Create(order);
        }

        public object GetSubscriptions(string CustomerId)
        {
            try
            {
                var obj = ApplicationDomain.Instance.PartnerCenterClient.Customers.ById(CustomerId).Subscriptions.Get().Items;
                return obj;
            }
            catch
            {
                return null;
            }
        }

        public object EditSubscriptions(string customerId, string subscriptionId)
        {
            try
            {
                var obj = ApplicationDomain.Instance.PartnerCenterClient.Customers.ById(customerId).Subscriptions.ById(subscriptionId);
                var obj1 = obj.Get();
                var obj2 = obj.AddOns.Get();
                return obj;
            }
            catch
            {
                return null;
            }
        }
    }
}