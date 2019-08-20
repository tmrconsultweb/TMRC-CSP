using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public enum LicenseType
    {
        NON_SPECIFIC = 1,
        ADDON = 2,
        TRIAL = 3
    }
    public static class LicenseTypes
    {
        public static LicenseType ParseEnum<LicenseType>(string value)
        {
            if (value.Contains("-"))
                value = value.Replace("-", "_").ToString();
            return (LicenseType)Enum.Parse(typeof(LicenseType), value, true);
        }

        public static string ParseEnumToString<LicenseType>(int value)
        {
            string lincense = Enum.GetName(typeof(TMRC_CSP.Models.LicenseType), value);
            if (!string.IsNullOrEmpty(lincense) && lincense.Contains("_"))
                lincense = lincense.Replace("_", "-");
            return lincense;
        }
    }
}