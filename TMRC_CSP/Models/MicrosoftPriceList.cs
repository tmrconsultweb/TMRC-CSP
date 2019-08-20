using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    // Db Model
    [Table("tbl_MicrosoftPriceList")]
    public class MicrosoftPriceList
    {
        [Key]
        public int Id { get; set; }
        public string MicrosoftId { get; set; }
        // Microsoft Offer name
        public string Name { get; set; }
        //Valid From Date
        public DateTime StartDate { get; set; }
        //Valid To Date
        public DateTime EndDate { get; set; }
        //Orginal Price
        public double Price { get; set; }
        // Customer Price
        public double CustomerPrice { get; set; }
        //Reseller Price
        public double ResellerPrice { get; set; }
        // License Agreement Type
        public int AgreementType { get; set; }
        //Secondary License Type
        public int LicenseType { get; set; }
        //End Customer Type
        public int CustomerType { get; set; }
        //Purchase Unit
        public int PurchaseUnit { get; set; }
        //Purchase Unit Number
        public int PurchaseUnitNumber { get; set; }
        // Status
        public bool Status { get; set; }
    }


    //Excel Model
    public class ExcelPriceList
    {
        public int Id { get; set; }
        // Action type like ('ADD','CHG','DEL','UNC')
        public int ActionType { get; set; }

        public string MicrosoftId { get; set; }
        // Microsoft Offer name
        public string Name { get; set; }
        //Valid From Date
        public DateTime StartDate { get; set; }
        //Valid To Date
        public DateTime EndDate { get; set; }
        //Orginal Price
        public double Price { get; set; }
        // Customer Price
        public double CustomerPrice { get; set; }
        //Reseller Price
        public double ResellerPrice { get; set; }
        // License Agreement Type
        public int AgreementType { get; set; }
        //Secondary License Type
        public int LicenseType { get; set; }
        //End Customer Type
        public int CustomerType { get; set; }
        //Purchase Unit
        public int PurchaseUnit { get; set; }
        //Purchase Unit Number
        public int PurchaseUnitNumber { get; set; }
    }


    //Grid Model
    public class GridPriceList
    {
        public int Id { get; set; }
        // Action type like ('ADD','CHG','DEL','UNC')
        public int ActionType { get; set; }
        //Offer Id /Microsoft Id
        public string MicrosoftId { get; set; }

        // Microsoft Offer name
        public string Name { get; set; }

        //Valid From Date
        public DateTime StartDate { get; set; }
        //Valid To Date
        public DateTime EndDate { get; set; }

        //public string Date { get; set; }

        //Orginal Price(Admin Buying Price)
        public double Price { get; set; }
        // Customer Price(Customer Buying Price)
        public double CustomerPrice { get; set; }
        //Percentage of Customer Price
        public string CustomerPricePercentage { get; set; }
        //Reseller Price(Reseller Buying Price)
        public double ResellerPrice { get; set; }
        //Percentage of Reseller Price
        public string ResellerPricePercentage { get; set; }
        // License Agreement Type
        public string AgreementType { get; set; }
        //Secondary License Type
        public string LicenseType { get; set; }
        //End Customer Type
        public string CustomerType { get; set; }
        //Purchase Unit
        public string PurchaseUnit { get; set; }
        //Purchase Unit Number
        public int PurchaseUnitNumber { get; set; }

        //Default percentage ERP - Reseller gape
        public string DefaultMarginReseller { get; set; }
    }
}