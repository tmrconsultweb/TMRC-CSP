using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_InvoiceSetting")]
    public class InvoiceSetting
    {
        [Key]
        public int Id { get; set; }
        public int ResellerId { get; set; }
        public int ExpiryDateOfInvoice { get; set; }
        public string InvoiceNotes { get; set; }
        public bool IsShowExpiryDate { get; set; }
        public bool IsShowIssueDate { get; set; }
        public bool IsShowCurrency { get; set; }
        public bool IsShowPOBox { get; set; }
        public bool IsShowLogo { get; set; }
        public bool IsShowNotes { get; set; }
        public bool IsShowAccNo { get; set; }
        public bool IsShowIBAN { get; set; }
        public bool IsShowSWIFT { get; set; }
        public string AccNo { get; set; }
        public string IBAN { get; set; }
        public string SWIFT { get; set; }
    }
}