using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.ViewModel.Invoice
{
    public class InvoiceSetting
    {
        public string Save(Models.InvoiceSetting _i, int ResellerId)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                if (_i.Id != 0 && db.InvoiceSetting.Any(m => m.Id == _i.Id))  //Update
                {
                    var invset = db.InvoiceSetting.Where(m => m.Id == _i.Id).FirstOrDefault();
                    invset.ExpiryDateOfInvoice = _i.ExpiryDateOfInvoice;
                    invset.InvoiceNotes = _i.InvoiceNotes;
                    invset.IsShowCurrency = _i.IsShowCurrency;
                    invset.IsShowExpiryDate = _i.IsShowExpiryDate;
                    invset.IsShowIssueDate = _i.IsShowIssueDate;
                    invset.IsShowLogo = _i.IsShowLogo;
                    invset.IsShowNotes = _i.IsShowNotes;
                    invset.IsShowPOBox = _i.IsShowPOBox;
                    invset.AccNo = _i.AccNo;
                    invset.IBAN = _i.IBAN;
                    invset.SWIFT = _i.SWIFT;
                    invset.IsShowAccNo = _i.IsShowAccNo;
                    invset.IsShowIBAN = _i.IsShowIBAN;
                    invset.IsShowSWIFT = _i.IsShowSWIFT;
                }
                else  //save
                {
                    Models.InvoiceSetting invset = new Models.InvoiceSetting
                    {
                        ExpiryDateOfInvoice = _i.ExpiryDateOfInvoice,
                        InvoiceNotes = _i.InvoiceNotes,
                        IsShowCurrency = _i.IsShowCurrency,
                        IsShowExpiryDate = _i.IsShowExpiryDate,
                        IsShowIssueDate = _i.IsShowIssueDate,
                        IsShowLogo = _i.IsShowLogo,
                        IsShowNotes = _i.IsShowNotes,
                        IsShowPOBox = _i.IsShowPOBox,
                        AccNo = _i.AccNo,
                        IBAN = _i.IBAN,
                        SWIFT = _i.SWIFT,
                        IsShowAccNo = _i.IsShowAccNo,
                        IsShowIBAN = _i.IsShowIBAN,
                        IsShowSWIFT = _i.IsShowSWIFT,
                    };
                    db.InvoiceSetting.Add(invset);
                }

                db.SaveChanges();
                return "Successfully Invoice setting has been saved.";
            }
            catch (Exception ex)
            {
                return "Unknown error occur, Please try again.";
            }
        }

        public Models.InvoiceSetting Get(int ResellerId)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                if (!db.InvoiceSetting.Any(m => m.ResellerId == ResellerId))
                    return EmptyInvoiceSetting();
                return db.InvoiceSetting.Where(m => m.ResellerId == ResellerId).FirstOrDefault();
            }
            catch
            {
                return EmptyInvoiceSetting();
            }
        }

        public Models.InvoiceSetting EmptyInvoiceSetting()
        {
            return new Models.InvoiceSetting
            {
                Id = 0,
            };
        }
    }
}