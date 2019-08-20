using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public enum PurchaseUnit
    {
        Day = 1,
        Months = 2,
        Years = 3
    }

    public static class PurchaseUnits
    {
        public static PurchaseUnit ParseEnum<PurchaseUnit>(string value)
        {
            if (value.Contains("("))
                value = value.Replace("(", "").ToString();
            if (value.Contains(")"))
                value = value.Replace(")", "").ToString();
            return (PurchaseUnit)Enum.Parse(typeof(PurchaseUnit), value, true);
        }

        public static string ParseEnumToString<PurchaseUnit>(int value)
        {
            string purchaseUnit = Enum.GetName(typeof(TMRC_CSP.Models.PurchaseUnit), value);
            return purchaseUnit;
        }
    }
}